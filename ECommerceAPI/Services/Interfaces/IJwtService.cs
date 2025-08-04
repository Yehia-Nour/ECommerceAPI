using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Customer customer);
    }
}
