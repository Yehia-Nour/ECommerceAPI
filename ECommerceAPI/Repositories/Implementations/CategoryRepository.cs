using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Category> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> CategoryNameExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(c => c.IsActive && c.Name.ToLower() == name.ToLower());
        }
    }
}
