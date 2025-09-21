using ECommerce.Application.Interfaces.Repositories;

namespace ECommerce.Application.Interfaces.UnitOfWork;

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
    IFeedbackRepository Feedbacks { get; }

    Task ExecuteInTransactionAsync(Func<Task> action);
    Task<int> SaveChangesAsync();
}