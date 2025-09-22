using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Address> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

}