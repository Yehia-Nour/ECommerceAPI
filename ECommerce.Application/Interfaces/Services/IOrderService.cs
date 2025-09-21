using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.OrderDTOs;

namespace ECommerce.Application.Interfaces.Services;

public interface IOrderService
{
    Task<ApiResponse<List<OrderResponseDTO>>> GetAllOrdersAsync();
    Task<ApiResponse<OrderResponseDTO>> GetOrderByIdAsync(int orderId);
    Task<ApiResponse<OrderResponseDTO>> CreateOrderAsync(OrderCreateDTO orderDto, int customerId);
    Task<ApiResponse<ConfirmationResponseDTO>> UpdateOrderStatusAsync(OrderStatusUpdateDTO statusDto);
    Task<ApiResponse<List<OrderResponseDTO>>> GetOrdersByCustomerAsync(int customerId);
}