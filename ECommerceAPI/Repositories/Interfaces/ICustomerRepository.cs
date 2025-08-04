using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<bool> CustomerExistsByEmailAsync(string email);
        
    }
}
