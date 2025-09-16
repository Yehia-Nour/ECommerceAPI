using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Reflection.Metadata.Ecma335;

namespace ECommerceAPI.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Payment> _dbSet;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Payment>();
        }

        public async Task<Payment?> GetByIdAsync(int paymentId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }
        public async Task AddAsync(Payment payment)
        {
            await _dbSet.AddAsync(payment);
        }

        public void Update(Payment payment)
        {
            _dbSet.Update(payment);
        }

        public async Task<Payment?> GetPaymentByOrderIdAsync(int orderId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<Payment?> GetPaymentWithOrderAsync(int paymentId)
        {
            return await _dbSet
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }
    }
}
