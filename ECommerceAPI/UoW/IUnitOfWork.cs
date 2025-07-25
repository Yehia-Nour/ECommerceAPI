using ECommerceAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ECommerceAPI.UoW
{
    public interface IUnitOfWork
    {
        public ICustomerRepository Customers { get; }
        Task<int> SaveChangesAsync();
    }
}
