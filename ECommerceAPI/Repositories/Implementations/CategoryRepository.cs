using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository, IGetAllRepository<Category>
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Category> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
