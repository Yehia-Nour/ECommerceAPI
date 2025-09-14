using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.OrderDTOs;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<List<OrderResponseDTO>>> GetAllOrdersAsync();
        Task<ApiResponse<OrderResponseDTO>> GetOrderByIdAsync(int orderId);
        Task<ApiResponse<OrderResponseDTO>> CreateOrderAsync(OrderCreateDTO orderDto, int customerId);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateOrderStatusAsync(OrderStatusUpdateDTO statusDto);
        Task<ApiResponse<List<OrderResponseDTO>>> GetOrdersByCustomerAsync(int customerId);
    }
}
