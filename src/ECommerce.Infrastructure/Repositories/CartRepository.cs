using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Cart?> GetCartByCustomerIdAsync(int customerId)
    {
        return await _dbSet
             .Include(c => c.CartItems)
             .ThenInclude(ci => ci.Product)
             .FirstOrDefaultAsync(c => c.CustomerId == customerId && !c.IsCheckedOut);
    }
}