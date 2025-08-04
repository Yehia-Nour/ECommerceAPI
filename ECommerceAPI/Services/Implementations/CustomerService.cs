using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;
using System.Runtime.CompilerServices;

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
        public async Task<bool> CheckCustomerExistsByEmailAsync(string email)
        {
            return await _unitOfWork.Customers.CustomerExistsByEmailAsync(email);
        }

        public Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {

            throw new NotImplementedException();
        }
        public async Task<Customer> CreateCustomerAsync(CustomerRegistrationDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

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


    }
}
//namespace ECommerceApp.Services
//{
//    public class CustomerService
//    {
//        private readonly ApplicationDbContext _context;
//        public CustomerService(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
//        {
//            try
//            {
//                var customer = await _context.Customers
//                .AsNoTracking()
//                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
//                if (customer == null)
//                {
//                    return new ApiResponse<CustomerResponseDTO>(404, "Customer not found.");
//                }
//                // Map to CustomerResponseDTO
//                var customerResponse = new CustomerResponseDTO
//                {
//                    Id = customer.Id,
//                    FirstName = customer.FirstName,
//                    LastName = customer.LastName,
//                    Email = customer.Email,
//                    PhoneNumber = customer.PhoneNumber,
//                    DateOfBirth = customer.DateOfBirth
//                };
//                return new ApiResponse<CustomerResponseDTO>(200, customerResponse);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                return new ApiResponse<CustomerResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
//            }
//        }
//        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto)
//        {
//            try
//            {
//                var customer = await _context.Customers.FindAsync(customerDto.CustomerId);
//                if (customer == null)
//                {
//                    return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found.");
//                }
//                // Check if email is being updated to an existing one
//                if (customer.Email != customerDto.Email && await _context.Customers.AnyAsync(c => c.Email == customerDto.Email))
//                {
//                    return new ApiResponse<ConfirmationResponseDTO>(400, "Email is already in use.");
//                }
//                // Update customer properties manually
//                customer.FirstName = customerDto.FirstName;
//                customer.LastName = customerDto.LastName;
//                customer.Email = customerDto.Email;
//                customer.PhoneNumber = customerDto.PhoneNumber;
//                customer.DateOfBirth = customerDto.DateOfBirth;
//                await _context.SaveChangesAsync();
//                // Prepare confirmation message
//                var confirmationMessage = new ConfirmationResponseDTO
//                {
//                    Message = $"Customer with Id {customerDto.CustomerId} updated successfully."
//                };
//                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
//            }
//        }
//        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(int id)
//        {
//            try
//            {
//                var customer = await _context.Customers
//                .FirstOrDefaultAsync(c => c.Id == id);
//                if (customer == null)
//                {
//                    return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found.");
//                }
//                //Soft Delete
//                customer.IsActive = false;
//                await _context.SaveChangesAsync();
//                // Prepare confirmation message
//                var confirmationMessage = new ConfirmationResponseDTO
//                {
//                    Message = $"Customer with Id {id} deleted successfully."
//                };
//                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
//            }
//        }
//        // Changes the password for an existing customer.
//        public async Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto)
//        {
//            try
//            {
//                var customer = await _context.Customers.FindAsync(changePasswordDto.CustomerId);
//                if (customer == null || !customer.IsActive)
//                {
//                    return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found or inactive.");
//                }
//                // Verify current password
//                bool isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, customer.Password);
//                if (!isCurrentPasswordValid)
//                {
//                    return new ApiResponse<ConfirmationResponseDTO>(401, "Current password is incorrect.");
//                }
//                // Hash the new password
//                customer.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
//                await _context.SaveChangesAsync();
//                // Prepare confirmation message
//                var confirmationMessage = new ConfirmationResponseDTO
//                {
//                    Message = "Password changed successfully."
//                };
//                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (implementation depends on your logging setup)
//                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
//            }
//        }
//    }
//}