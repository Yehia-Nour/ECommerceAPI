using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>, IGetAllRepository<Product>
    {
        Task<bool> ProductNameExistsAsync(string name);
    }
}
