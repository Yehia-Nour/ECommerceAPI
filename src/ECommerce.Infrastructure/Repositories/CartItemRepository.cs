using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

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