using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Implementations
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CartItem> _dbSet;
        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CartItem>();
        }
        public async Task AddAsync(CartItem cartItem)
        {
            await _dbSet.AddAsync(cartItem);
        }

        public void Update(CartItem cartItem)
        {
            _dbSet.Update(cartItem);
        }

        public void Delete(CartItem cartItem)
        {
            _dbSet.Remove(cartItem);
        }
        public void RemoveRange(IEnumerable<CartItem> cartItems)
        {
            _dbSet.RemoveRange(cartItems);
        }

    }
}
