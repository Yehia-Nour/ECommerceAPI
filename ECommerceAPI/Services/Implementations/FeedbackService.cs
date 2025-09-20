using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.FeedbackDTOs;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {

        public async Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbackAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<FeedbackResponseDTO>> SubmitFeedbackAsync(FeedbackCreateDTO feedbackCreateDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ProductFeedbackResponseDTO>> GetFeedbackForProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<FeedbackResponseDTO>> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackUpdateDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(int feedbackId, int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
