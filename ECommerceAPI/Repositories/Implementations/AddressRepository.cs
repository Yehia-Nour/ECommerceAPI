using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        protected readonly DbSet<Address> _dbSet;
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Address>();
        }

        public IQueryable<Address> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

    }
}
