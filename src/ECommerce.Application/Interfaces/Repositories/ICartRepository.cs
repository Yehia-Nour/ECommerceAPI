using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByCustomerIdAsync(int customerId);
    }