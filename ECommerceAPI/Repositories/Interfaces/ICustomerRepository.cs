using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}
