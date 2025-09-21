using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IGetAllRepository<T> where T : class
{
    IQueryable<T> GetAll();
}