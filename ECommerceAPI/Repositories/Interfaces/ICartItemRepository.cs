using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task AddAsync(CartItem cartItem);
        void Update(CartItem cartItem);
        void Delete(CartItem cartItem);
        void RemoveRange(IEnumerable<CartItem> cartItems);
    }
}
