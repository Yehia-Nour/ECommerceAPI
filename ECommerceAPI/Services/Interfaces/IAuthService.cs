using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(CustomerRegistrationDTO customerDto);
        Task<ApiResponse<string>> LoginAsync(LoginDTO loginDto);
        Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto);
    }
}
