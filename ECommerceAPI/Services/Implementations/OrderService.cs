using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.OrderDTOs;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public Task<ApiResponse<List<OrderResponseDTO>>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<OrderResponseDTO>> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<OrderResponseDTO>> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> UpdateOrderStatusAsync(OrderStatusUpdateDTO statusDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<OrderResponseDTO>>> GetOrdersByCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
