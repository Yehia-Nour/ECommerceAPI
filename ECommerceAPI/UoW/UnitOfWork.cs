
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories;
using ECommerceAPI.Repositories.Implementations;
using ECommerceAPI.Repositories.Interfaces;

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



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Addresses = new AddressRepository(_context);
            Categories = new CategoryRepository(_context);
            Products= new ProductRepository(_context);
            Carts = new CartRepository(_context);
            CartItems = new CartItemRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
