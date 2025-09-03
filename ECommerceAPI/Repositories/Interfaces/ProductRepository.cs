using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Implementations;

namespace ECommerceAPI.Repositories.Interfaces
{
    public class ProductRepository :BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
