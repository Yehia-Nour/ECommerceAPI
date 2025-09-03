using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerceAPI.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
