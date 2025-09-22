using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(int paymentId);
    Task AddAsync(Payment payment);
    void Update(Payment payment);
    Task<Payment?> GetPaymentByOrderIdAsync(int orderId);
    Task<Payment?> GetPaymentWithOrderAsync(int paymentId);
    Task<List<Payment>> GetPendingPaymentsAsync();
}