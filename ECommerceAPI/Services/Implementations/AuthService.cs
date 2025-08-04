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
            if (await _customerService.CheckCustomerExistsByEmailAsync(customerDto.CustomerEmail))
                return new ApiResponse<string>(400, "Email already exists.");
            var customer = await _customerService.CreateCustomerAsync(customerDto);
            var token = _jwtService.GenerateToken(customer);

            return new ApiResponse<string>(200, token);
        }

        public Task<ApiResponse<string>> LoginAsync(LoginDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
