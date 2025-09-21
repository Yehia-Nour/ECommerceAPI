using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Order> _dbSet;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Order>();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .Include(o => o.BillingAddress)
                .Include(o => o.ShippingAddress)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .Include(o => o.BillingAddress)
                .Include(o => o.ShippingAddress)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }


        public async Task AddAsync(Order order)
        {
            await _dbSet.AddAsync(order);
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Order?> GetOrderWithPaymentAsync(int orderId, int customerId)
        {
            return await _dbSet
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == customerId);
        }

        public async Task<Order?> GetOrderByIdAndCustomerIdAsync(int orderId, int customerId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == customerId);
        }
    }