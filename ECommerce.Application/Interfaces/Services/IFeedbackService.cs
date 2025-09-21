using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.FeedbackDTOs;

namespace ECommerce.Application.Interfaces.Services;

public interface IFeedbackService
{
    Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbackAsync();
    Task<ApiResponse<FeedbackResponseDTO>> SubmitFeedbackAsync(FeedbackCreateDTO feedbackCreateDTO, int customerId);
    Task<ApiResponse<ProductFeedbackResponseDTO>> GetFeedbackForProductAsync(int productId);
    Task<ApiResponse<FeedbackResponseDTO>> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackUpdateDTO, int customerId);
    Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(int feedbackId, int customerId);
}