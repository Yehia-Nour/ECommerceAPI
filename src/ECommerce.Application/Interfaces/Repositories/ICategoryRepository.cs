using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>, IGetAllRepository<Category>
{
    Task<bool> CategoryNameExistsAsync(string name);
}