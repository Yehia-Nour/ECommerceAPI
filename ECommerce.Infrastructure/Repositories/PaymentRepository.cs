using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

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

        public async Task<List<Payment>> GetPendingPaymentsAsync()
        {
            return await _dbSet
                .Include(p => p.Order)
                .Where(p => p.Status == PaymentStatus.Pending &&
                            p.PaymentMethod.ToUpper() != "COD")
                .ToListAsync();
        }

    }