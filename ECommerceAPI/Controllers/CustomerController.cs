using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        private int GetCustomerId()
        {
            var CustomerIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(CustomerIdStr, out int CustomerId))
                throw new Exception("Invalid or missing customer ID in token.");

            return CustomerId;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<CustomerResponseDTO>>> GetCustomerById()
        {
            var customerId = GetCustomerId();
            var response = await _customerService.GetCustomerByIdAsync(customerId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCustomer([FromBody] CustomerUpdateDTO customerDto)
        {
            var customerId = GetCustomerId();
            var response = await _customerService.UpdateCustomerAsync(customerDto, customerId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCustomer()
        {
            var customerId = GetCustomerId();
            var response = await _customerService.DeleteCustomerAsync(customerId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
