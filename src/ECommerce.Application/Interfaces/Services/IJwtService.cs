using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(Customer customer);
}