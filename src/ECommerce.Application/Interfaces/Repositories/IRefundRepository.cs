using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IRefundRepository
{
    Task<List<Refund>> GetAllAsync();
    Task AddAsync(Refund refund);
    void Update(Refund refund);
    Task<Refund?> GetRefundByCancellationIdAsync(int cancellationId);
    Task<Refund?> GetRefundWithOrderDetailsByIdAsync(int refundId);
    Task<Refund?> GetRefundWithDetailsByIdAsync(int refundId);
}