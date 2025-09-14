using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.OrderDTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Enums;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;
using Microsoft.EntityFrameworkCore;


namespace ECommerceAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<OrderResponseDTO>>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();

            if (orders == null || !orders.Any())
                return new ApiResponse<List<OrderResponseDTO>>(404, "No orders found.");

            var orderList = _mapper.Map<List<OrderResponseDTO>>(orders);

            return new ApiResponse<List<OrderResponseDTO>>(200, orderList);
        }

        public async Task<ApiResponse<OrderResponseDTO>> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

            if (order == null)
                return new ApiResponse<OrderResponseDTO>(404, "Order not found.");

            var orderResponse = _mapper.Map<OrderResponseDTO>(order);

            return new ApiResponse<OrderResponseDTO>(200, orderResponse);
        }

        public async Task<ApiResponse<OrderResponseDTO>> CreateOrderAsync(OrderCreateDTO orderDto, int customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer == null)
                return new ApiResponse<OrderResponseDTO>(404, "Customer does not exist.");


            var billingAddress = await _unitOfWork.Addresses.GetByIdAsync(orderDto.BillingAddressId);
            if (billingAddress == null)
                return new ApiResponse<OrderResponseDTO>(400, "Billing Address is invalid or does not belong to the customer.");


            var shippingAddress = await _unitOfWork.Addresses.GetByIdAsync(orderDto.ShippingAddressId);
            if (shippingAddress == null)
                return new ApiResponse<OrderResponseDTO>(400, "Shipping Address is invalid or does not belong to the customer.");


            decimal totalBaseAmount = 0;
            decimal totalDiscountAmount = 0;
            decimal shippingCost = 10.00m;
            decimal totalAmount = 0;

            string orderNumber = OrderUtility.GenerateOrderNumber();

            var orderItems = new List<OrderItem>();
            foreach (var itemDto in orderDto.OrderItems)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    return new ApiResponse<OrderResponseDTO>(404, $"Product with ID {itemDto.ProductId} does not exist.");


                if (product.StockQuantity < itemDto.Quantity)
                    return new ApiResponse<OrderResponseDTO>(400, $"Insufficient stock for product {product.Name}.");

                decimal basePrice = itemDto.Quantity * product.Price;
                decimal discount = (product.DiscountPercentage / 100.0m) * basePrice;
                decimal totalPrice = basePrice - discount;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price,
                    Discount = discount,
                    TotalPrice = totalPrice
                };
                orderItems.Add(orderItem);

                totalBaseAmount += basePrice;
                totalDiscountAmount += discount;

                product.StockQuantity -= itemDto.Quantity;

                _unitOfWork.Products.Update(product);
            }
            totalAmount = totalBaseAmount - totalDiscountAmount + shippingCost;

            var order = new Order
            {
                OrderNumber = orderNumber,
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                BillingAddressId = orderDto.BillingAddressId,
                ShippingAddressId = orderDto.ShippingAddressId,
                TotalBaseAmount = totalBaseAmount,
                TotalDiscountAmount = totalDiscountAmount,
                ShippingCost = shippingCost,
                TotalAmount = totalAmount,
                OrderStatus = OrderStatus.Pending,
                OrderItems = orderItems
            };

            await _unitOfWork.Orders.AddAsync(order);

            var cart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            if (cart != null)
            {
                cart.IsCheckedOut = true;
                cart.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Carts.Update(cart);
            }

            await _unitOfWork.SaveChangesAsync();

            var orderResponse = _mapper.Map<OrderResponseDTO>(order);

            return new ApiResponse<OrderResponseDTO>(200, orderResponse);
        }


        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateOrderStatusAsync(OrderStatusUpdateDTO statusDto)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(statusDto.OrderId);
            if (order == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Order not found.");
            var currentStatus = order.OrderStatus;
            var newStatus = statusDto.OrderStatus;

            if (!OrderUtility.AllowedStatusTransitions.TryGetValue(currentStatus, out var allowedStatuses))
                return new ApiResponse<ConfirmationResponseDTO>(500, "Current order status is invalid.");

            if (!OrderUtility.CanTransition(currentStatus, newStatus))
                return new ApiResponse<ConfirmationResponseDTO>(400, $"Cannot change order status from {currentStatus} to {newStatus}.");


            order.OrderStatus = newStatus;
            await _unitOfWork.SaveChangesAsync();

            var confirmation = new ConfirmationResponseDTO
            {
                Message = $"Order Status with Id {statusDto.OrderId} updated successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
        }

        public async Task<ApiResponse<List<OrderResponseDTO>>> GetOrdersByCustomerAsync(int customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer == null)
                return new ApiResponse<List<OrderResponseDTO>>(404, "Customer not found.");

            var orders = await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(customerId);

            if (orders == null || !orders.Any())
                return new ApiResponse<List<OrderResponseDTO>>(404, "No orders found for this customer.");

            var orderDtos = _mapper.Map<List<OrderResponseDTO>>(orders);

            return new ApiResponse<List<OrderResponseDTO>>(200, orderDtos);
        }

    }
}
