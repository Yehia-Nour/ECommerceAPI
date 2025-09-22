using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CancellationDTOs;
using ECommerce.Application.Helpers;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancellationsController : ControllerBase
    {
        private readonly ICancellationService _cancellationService;

        public CancellationsController(ICancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpPost("RequestCancellation")]
        public async Task<ActionResult<ApiResponse<CancellationResponseDTO>>> RequestCancellation([FromBody] CancellationRequestDTO cancellationRequest)
        {
            var customerId = User.GetCustomerId();
            var response = await _cancellationService.RequestCancellationAsync(cancellationRequest, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAllCancellations")]
        public async Task<ActionResult<ApiResponse<List<CancellationResponseDTO>>>> GetAllCancellations()
        {
            var response = await _cancellationService.GetAllCancellationsAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCancellationById/{id}")]
        public async Task<ActionResult<ApiResponse<CancellationResponseDTO>>> GetCancellationById(int id)
        {
            var response = await _cancellationService.GetCancellationByIdAsync(id);

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("UpdateCancellationStatus")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCancellationStatus([FromBody] CancellationStatusUpdateDTO statusUpdate)
        {
            var response = await _cancellationService.UpdateCancellationStatusAsync(statusUpdate);

            return StatusCode(response.StatusCode, response);
        }
    }
}
