using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task AddAsync(CartItem cartItem);
    void Update(CartItem cartItem);
    void Delete(CartItem cartItem);
    void RemoveRange(IEnumerable<CartItem> cartItems);
}