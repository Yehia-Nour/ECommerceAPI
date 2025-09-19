using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetOrderItemsWithProductsAsync(int orderId);
    }
}
