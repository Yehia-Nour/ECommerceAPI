using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
    Task<List<Feedback>> GetAllAsync();
    Task<bool> ExistsForCustomerAndProductAsync(int customerId, int productId);
    Task<List<Feedback>> GetFeedbacksWithCustomerByProductIdAsync(int productId);
    Task<Feedback?> GetFeedbackWithDetailsByIdAndCustomerAsync(int feedbackId, int customerId);
}