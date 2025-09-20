using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Feedback> _dbSet;
        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Feedback>();
        }
    }
}
