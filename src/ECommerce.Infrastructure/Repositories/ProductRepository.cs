using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Product> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public async Task<bool> ProductNameExistsAsync(string name)
    {
        return await _dbSet.AnyAsync(p => p.IsAvailable && p.Name.ToLower() == name.ToLower());
    }
}