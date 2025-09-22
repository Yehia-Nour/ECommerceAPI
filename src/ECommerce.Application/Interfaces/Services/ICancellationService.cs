
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CancellationDTOs;

namespace ECommerce.Application.Interfaces.Services;

public interface ICancellationService
{
    Task<ApiResponse<List<CancellationResponseDTO>>> GetAllCancellationsAsync();
    Task<ApiResponse<CancellationResponseDTO>> GetCancellationByIdAsync(int id);
    Task<ApiResponse<CancellationResponseDTO>> RequestCancellationAsync(CancellationRequestDTO cancellationRequest, int customerId);
    Task<ApiResponse<ConfirmationResponseDTO>> UpdateCancellationStatusAsync(CancellationStatusUpdateDTO statusUpdate);
}