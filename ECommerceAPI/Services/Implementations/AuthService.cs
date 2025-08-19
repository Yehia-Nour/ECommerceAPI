using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerService _customerService;
        private readonly IJwtService _jwtService;
        public AuthService(ICustomerService customerService, IJwtService jwtService)
        {
            _customerService = customerService;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<string>> RegisterAsync(CustomerRegistrationDTO customerDto)
        {
            var customer = await _customerService.GetCustomerByEmailAsync(customerDto.CustomerEmail);
            if (customer is not null)
                return new ApiResponse<string>(400, "Email already exists.");
            customer = await _customerService.CreateCustomerAsync(customerDto);
            var token = _jwtService.GenerateToken(customer);

            return new ApiResponse<string>(200, token, true);
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDTO loginDto)
        {
            var customer = await _customerService.GetCustomerByEmailAsync(loginDto.Email);
            if (customer is null)
                return new ApiResponse<string>(401, "Invalid email or password.");
            if (!customer.IsActive)
                return new ApiResponse<string>(403, "Account is deactivated.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, customer.Password);
            if (!isPasswordValid)
                return new ApiResponse<string>(401, "Invalid email or password.");

            var token = _jwtService.GenerateToken(customer);

            return new ApiResponse<string>(200, token, true);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto)
        {
            var customer = await _customerService.GetCustomerByEmailAsync(changePasswordDto.CustomerEmail);
            if (customer is null || !customer.IsActive)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found or inactive.");


            bool isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, customer.Password);
            if (!isCurrentPasswordValid)
                return new ApiResponse<ConfirmationResponseDTO>(401, "Current password is incorrect.");

            await _customerService.UpdateCustomerPasswordAsync(customer, changePasswordDto.NewPassword);
            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = "Password changed successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage, true);
        }
    }
}
