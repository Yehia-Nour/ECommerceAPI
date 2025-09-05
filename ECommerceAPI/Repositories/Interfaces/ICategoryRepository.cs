using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Implementations;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>, IGetAllRepository<Category>
    {
        Task<bool> CategoryNameExistsAsync(string name);
    }
}
