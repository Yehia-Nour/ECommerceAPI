using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IRefundRepository
    {
        Task AddAsync(Refund refund);
        Task<Refund?> GetRefundByCancellationIdAsync(int cancellationId);
        Task<Refund?> GetRefundWithDetailsByIdAsync(int refundId);
    }
}
