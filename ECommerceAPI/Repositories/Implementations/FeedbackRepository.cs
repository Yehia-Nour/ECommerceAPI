using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _dbSet
                .Include(f => f.Customer)
                .Include(f => f.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsForCustomerAndProductAsync(int customerId, int productId)
        {
            return await _dbSet.AnyAsync(f =>
                f.CustomerId == customerId &&
                f.ProductId == productId);
        }

        public async Task<List<Feedback>> GetFeedbacksWithCustomerByProductIdAsync(int productId)
        {
            return await _dbSet
                .Where(f => f.ProductId == productId)
                .Include(f => f.Customer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackWithDetailsByIdAndCustomerAsync(int feedbackId, int customerId)
        {
            return await _dbSet
                .Include(f => f.Customer)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f => f.Id == feedbackId && f.CustomerId == customerId);
        }
    }
}
