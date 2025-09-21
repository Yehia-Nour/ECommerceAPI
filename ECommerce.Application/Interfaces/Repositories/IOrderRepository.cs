using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int orderId);
    Task AddAsync(Order order);
    Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
    Task<Order?> GetOrderWithPaymentAsync(int orderId, int customerId);
    Task<Order?> GetOrderByIdAndCustomerIdAsync(int orderId, int customerId);

}