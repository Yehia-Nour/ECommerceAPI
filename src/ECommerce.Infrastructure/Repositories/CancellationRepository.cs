using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CancellationRepository : ICancellationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Cancellation> _dbSet;
    public CancellationRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Cancellation>();
    }

    public async Task<List<Cancellation>> GetAllWithOrdersAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .Include(c => c.Order)
            .ToListAsync();
    }

    public async Task<Cancellation?> GetByIdAsync(int id)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Cancellation cancellation)
    {
        await _dbSet.AddAsync(cancellation);
    }

    public void Update(Cancellation cancellation)
    {
        _dbSet.Update(cancellation);
    }

    public async Task<Cancellation?> GetCancellationByOrderIdAsync(int orderId)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.OrderId == orderId);
    }

    public async Task<Cancellation?> GetCancellationWithOrderAndCustomerAsync(int cancellationId)
    {
        return await _dbSet
            .Include(c => c.Order)
            .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(c => c.Id == cancellationId);
    }

    public async Task<Cancellation?> GetCancellationWithDetailsByIdAsync(int cancellationId)
    {
        return await _dbSet
            .Include(c => c.Order)
            .ThenInclude(o => o.Payment)
            .Include(c => c.Order)
            .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(c => c.Id == cancellationId);
    }

    public async Task<List<Cancellation>> GetEligibleRefundsAsync()
    {
        return await _dbSet
            .Include(c => c.Order)
            .ThenInclude(o => o.Payment)
            .Where(c => c.Status == CancellationStatus.Approved
                        && c.Refund == null
                        && c.Order.Payment.PaymentMethod.ToLower() != "cod")
            .ToListAsync();
    }
}