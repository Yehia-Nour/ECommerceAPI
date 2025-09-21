using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface ICancellationRepository
{
    Task<List<Cancellation>> GetAllWithOrdersAsync();
    Task<Cancellation?> GetByIdAsync(int id);
    Task AddAsync(Cancellation cancellation);
    void Update(Cancellation cancellation);
    Task<Cancellation?> GetCancellationByOrderIdAsync(int orderId);
    Task<Cancellation?> GetCancellationWithOrderAndCustomerAsync(int cancellationId);
    Task<Cancellation?> GetCancellationWithDetailsByIdAsync(int cancellationId);
    Task<List<Cancellation>> GetEligibleRefundsAsync();
}