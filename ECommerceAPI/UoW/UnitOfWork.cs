using ECommerceAPI.Data;
using ECommerceAPI.Repositories.Implementations;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerceAPI.UoW
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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Addresses = new AddressRepository(_context);
            Categories = new CategoryRepository(_context);
            Products = new ProductRepository(_context);
            Carts = new CartRepository(_context);
            CartItems = new CartItemRepository(_context);
            Orders = new OrderRepository(_context);
            Payments = new PaymentRepository(_context);
            Cancellations = new CancellationRepository(_context);
            OrderItems = new OrderItemRepository(_context);
            Refunds = new RefundRepository(_context);
            Feedbacks = new FeedbackRepository(_context);
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
