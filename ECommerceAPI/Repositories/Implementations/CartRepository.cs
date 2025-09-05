using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Cart?> GetCartByCustomerIdAsync(int customerId)
        {
           return  await _dbSet
                .Include(c => c.CartItems) 
                .ThenInclude(ci => ci.Product) 
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && !c.IsCheckedOut);
        }
    }
}
