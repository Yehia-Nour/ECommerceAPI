using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.RefundDTOs;
using ECommerceAPI.Services.Implementations;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundsController : ControllerBase
    {
        private readonly IRefundService _refundService;
        public RefundsController(IRefundService refundService)
        {
            _refundService = refundService;
        }

        [HttpGet("GetAllRefunds")]
        public async Task<ActionResult<ApiResponse<List<RefundResponseDTO>>>> GetAllRefunds()
        {
            var response = await _refundService.GetAllRefundsAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetRefundById/{id}")]
        public async Task<ActionResult<ApiResponse<RefundResponseDTO>>> GetRefundById(int id)
        {
            var response = await _refundService.GetRefundByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetEligibleRefunds")]
        public async Task<ActionResult<ApiResponse<List<PendingRefundResponseDTO>>>> GetEligibleRefunds()
        {
            var response = await _refundService.GetEligibleRefundsAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("ProcessRefund")]
        public async Task<ActionResult<ApiResponse<RefundResponseDTO>>> ProcessRefund([FromBody] RefundRequestDTO refundRequest)
        {
            var response = await _refundService.ProcessRefundAsync(refundRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateRefundStatus")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateRefundStatus([FromBody] RefundStatusUpdateDTO statusUpdate)
        {
            var response = await _refundService.UpdateRefundStatusAsync(statusUpdate);

            return StatusCode(response.StatusCode, response);
        }
    }
}
