using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IRefundRepository
    {
        Task<List<Refund>> GetAllAsync();
        Task AddAsync(Refund refund);
        void Update(Refund refund)
        Task<Refund?> GetRefundByCancellationIdAsync(int cancellationId);
        Task<Refund?> GetRefundWithOrderDetailsByIdAsync(int refundId);
        Task<Refund?> GetRefundWithDetailsByIdAsync(int refundId);
    }
}
