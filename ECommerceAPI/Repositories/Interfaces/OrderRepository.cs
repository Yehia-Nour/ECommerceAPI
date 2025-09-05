using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Implementations;

namespace ECommerceAPI.Repositories.Interfaces
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context){}

        public IQueryable<Order> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
