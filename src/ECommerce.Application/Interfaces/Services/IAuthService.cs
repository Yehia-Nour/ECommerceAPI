using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CustomerDTOs;

namespace ECommerce.Application.Interfaces.Services;

public interface IAuthService
{
    Task<ApiResponse<string>> RegisterAsync(CustomerRegistrationDTO customerDto);
    Task<ApiResponse<string>> LoginAsync(LoginDTO loginDto);
    Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto);
}
