using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class RefundRepository : IRefundRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Refund> _dbSet;
    public RefundRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Refund>();
    }

    public async Task<List<Refund>> GetAllAsync()
    {
        return await _dbSet.Include(r => r.Cancellation)
            .ThenInclude(c => c.Order)
            .ThenInclude(o => o.Payment)
            .ToListAsync();
    }

    public async Task AddAsync(Refund refund)
    {
        await _dbSet.AddAsync(refund);
    }

    public void Update(Refund refund)
    {
        _dbSet.Update(refund);
    }

    public async Task<Refund?> GetRefundByCancellationIdAsync(int cancellationId)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.CancellationId == cancellationId);
    }

    public async Task<Refund?> GetRefundWithOrderDetailsByIdAsync(int refundId)
    {
        return await _dbSet
            .Include(r => r.Cancellation)
            .ThenInclude(c => c.Order)
            .ThenInclude(o => o.Payment)
            .FirstOrDefaultAsync(r => r.Id == refundId);
    }

    public async Task<Refund?> GetRefundWithDetailsByIdAsync(int refundId)
    {
        return await _dbSet
            .Include(r => r.Cancellation)
            .ThenInclude(c => c.Order)
            .ThenInclude(o => o.Customer)
            .Include(r => r.Payment)
            .FirstOrDefaultAsync(r => r.Id == refundId);
    }

}