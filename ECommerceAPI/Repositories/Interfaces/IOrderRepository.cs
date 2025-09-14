using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.OrderDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int orderId);
        Task AddAsync(Order order);
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);

    }
}
