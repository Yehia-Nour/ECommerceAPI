using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>, IGetAllRepository<Product>
    {
        Task<bool> ProductNameExistsAsync(string name);
    }
}
