using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<ActionResult<ApiResponse<CustomerResponseDTO>>> GetCustomerById(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCustomer([FromBody] CustomerUpdateDTO customerDto)
        {
            var response = await _customerService.UpdateCustomerAsync(customerDto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCustomer(int id)
        {
            var response = await _customerService.DeleteCustomerAsync(id);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
