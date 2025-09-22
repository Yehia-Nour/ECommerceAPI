using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

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

        public async Task<OrderItem?> GetDeliveredOrderItemAsync(int productId, int customerId)
        {
            return await _dbSet
                .Include(oi => oi.Order)
                .AsNoTracking()
                .FirstOrDefaultAsync(oi =>
                    oi.ProductId == productId &&
                    oi.Order.CustomerId == customerId &&
                    oi.Order.OrderStatus == OrderStatus.Delivered);
        }
    }