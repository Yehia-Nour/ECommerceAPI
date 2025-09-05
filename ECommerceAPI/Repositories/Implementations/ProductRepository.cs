using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class ProductRepository :GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context){ }

        public IQueryable<Product> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> ProductNameExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(p => p.IsAvailable && p.Name.ToLower() == name.ToLower());
        }
    }
}
