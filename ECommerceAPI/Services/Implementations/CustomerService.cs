using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;

namespace ECommerceAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork uintOfWork, IMapper mapper)
        {
            _unitOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _unitOfWork.Customers.GetCustomerByEmailAsync(email);
        }

        public Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {

            throw new NotImplementedException();
        }

        public async Task<Customer> CreateCustomerAsync(CustomerRegistrationDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            customer.Password = BCrypt.Net.BCrypt.HashPassword(customerDto.CustomerPassword);

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer;
        }


        public Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCustomerPasswordAsync(Customer customer, string hashedNewPassword)
        {
            customer.Password = BCrypt.Net.BCrypt.HashPassword(hashedNewPassword);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}