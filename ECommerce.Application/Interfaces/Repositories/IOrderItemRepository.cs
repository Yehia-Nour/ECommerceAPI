using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetOrderItemsWithProductsAsync(int orderId);
    Task<OrderItem?> GetDeliveredOrderItemAsync(int productId, int customerId);
}