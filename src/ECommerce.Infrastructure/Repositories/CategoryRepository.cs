using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

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