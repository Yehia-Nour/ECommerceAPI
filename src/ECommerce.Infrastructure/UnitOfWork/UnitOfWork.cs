using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICustomerRepository Customers { get; }
        public IAddressRepository Addresses { get; }
        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public ICartRepository Carts { get; }
        public ICartItemRepository CartItems { get; }
        public IOrderRepository Orders { get; }
        public IPaymentRepository Payments { get; }
        public ICancellationRepository Cancellations { get; }
        public IOrderItemRepository OrderItems { get; }
        public IRefundRepository Refunds { get; }
        public IFeedbackRepository Feedbacks { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            ICustomerRepository customers,
            IAddressRepository addresses,
            ICategoryRepository categories,
            IProductRepository products,
            ICartRepository carts,
            ICartItemRepository cartItems,
            IOrderRepository orders,
            IPaymentRepository payments,
            ICancellationRepository cancellations,
            IOrderItemRepository orderItems,
            IRefundRepository refunds,
            IFeedbackRepository feedbacks)
        {
            _context = context;
            Customers = customers;
            Addresses = addresses;
            Categories = categories;
            Products = products;
            Carts = carts;
            CartItems = cartItems;
            Orders = orders;
            Payments = payments;
            Cancellations = cancellations;
            OrderItems = orderItems;
            Refunds = refunds;
            Feedbacks = feedbacks;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
