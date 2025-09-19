using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<OrderItem> _dbSet;
        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<OrderItem>();
        }

        public async Task<List<OrderItem>> GetOrderItemsWithProductsAsync(int orderId)
        {
            return await _dbSet
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }
    }
}
