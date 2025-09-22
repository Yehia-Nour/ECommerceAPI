using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>, IGetAllRepository<Product>
{
    Task<bool> ProductNameExistsAsync(string name);
}