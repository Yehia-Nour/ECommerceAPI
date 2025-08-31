using ECommerceAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ECommerceAPI.UoW
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IAddressRepository Addresses { get; }
        Task<int> SaveChangesAsync();
    }
}
