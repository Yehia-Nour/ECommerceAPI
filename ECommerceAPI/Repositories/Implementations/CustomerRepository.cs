using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerceAPI.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        protected readonly DbSet<Customer> _dbSet;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Customer>();
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Email == email);
        }

    }
}
