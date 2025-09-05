using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.OrderDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>, IGetAllRepository<Order>
    {
    }
}
