using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CancellationDTOs;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICancellationService
    {
        Task<ApiResponse<List<CancellationResponseDTO>>> GetAllCancellationsAsync();
        Task<ApiResponse<CancellationResponseDTO>> GetCancellationByIdAsync(int id);
        Task<ApiResponse<CancellationResponseDTO>> RequestCancellationAsync(CancellationRequestDTO cancellationRequest, int customerId);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCancellationStatusAsync(CancellationStatusUpdateDTO statusUpdate);
    }
}
