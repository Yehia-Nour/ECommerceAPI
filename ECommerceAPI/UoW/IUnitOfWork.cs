using ECommerceAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ECommerceAPI.UoW
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IAddressRepository Addresses { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        ICartItemRepository CartItems { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync();
    }
}
