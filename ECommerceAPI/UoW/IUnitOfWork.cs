using ECommerceAPI.Repositories.Implementations;
using ECommerceAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ECommerceAPI.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IAddressRepository Addresses { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        ICartItemRepository CartItems { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        ICancellationRepository Cancellations { get; }
        IOrderItemRepository OrderItems { get; }
        IRefundRepository Refunds { get; }

        Task ExecuteInTransactionAsync(Func<Task> action);
        Task<int> SaveChangesAsync();
    }
}
