using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.FeedbackDTOs;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbackAsync();
        Task<ApiResponse<FeedbackResponseDTO>> SubmitFeedbackAsync(FeedbackCreateDTO feedbackCreateDTO, int customerId);
        Task<ApiResponse<ProductFeedbackResponseDTO>> GetFeedbackForProductAsync(int productId);
        Task<ApiResponse<FeedbackResponseDTO>> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackUpdateDTO, int customerId);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(int feedbackId, int customerId);
    }
}
