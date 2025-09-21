using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IAddressRepository : IGenericRepository<Address>, IGetAllRepository<Address>
{
}
