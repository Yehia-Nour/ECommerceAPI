using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
    }
}
