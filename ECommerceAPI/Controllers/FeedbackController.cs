using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.FeedbackDTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("SubmitFeedback")]
        public async Task<ActionResult<ApiResponse<FeedbackResponseDTO>>> SubmitFeedback([FromBody] FeedbackCreateDTO feedbackCreateDTO)
        {
            var customerId = User.GetCustomerId();
            var response = await _feedbackService.SubmitFeedbackAsync(feedbackCreateDTO, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetFeedbackForProduct/{productId}")]
        public async Task<ActionResult<ApiResponse<ProductFeedbackResponseDTO>>> GetFeedbackForProduct(int productId)
        {
            var response = await _feedbackService.GetFeedbackForProductAsync(productId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAllFeedback")]
        public async Task<ActionResult<ApiResponse<List<FeedbackResponseDTO>>>> GetAllFeedback()
        {
            var response = await _feedbackService.GetAllFeedbackAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateFeedback")]
        public async Task<ActionResult<ApiResponse<FeedbackResponseDTO>>> UpdateFeedback([FromBody] FeedbackUpdateDTO feedbackUpdateDTO)
        {
            var customerId = User.GetCustomerId();
            var response = await _feedbackService.UpdateFeedbackAsync(feedbackUpdateDTO, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("DeleteFeedback/{feedbackId}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteFeedback(int feedbackId)
        {
            var customerId = User.GetCustomerId();
            var response = await _feedbackService.DeleteFeedbackAsync(feedbackId, customerId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
