using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICancellationRepository
    {
        Task<List<Cancellation>> GetAllWithOrdersAsync();
        Task<Cancellation?> GetByIdAsync(int id);
        Task AddAsync(Cancellation cancellation);
        void Update(Cancellation cancellation);
        Task<Cancellation?> GetCancellationByOrderIdAsync(int orderId);
        Task<Cancellation?> GetCancellationWithOrderAndCustomerAsync(int cancellationId);
    }
}
