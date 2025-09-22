using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Helpers;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<CustomerResponseDTO>>> GetCustomerById()
        {
            var customerId = User.GetCustomerId();
            var response = await _customerService.GetCustomerByIdAsync(customerId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCustomer([FromBody] CustomerUpdateDTO customerDto)
        {
            var customerId = User.GetCustomerId();
            var response = await _customerService.UpdateCustomerAsync(customerDto, customerId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCustomer()
        {
            var customerId = User.GetCustomerId();
            var response = await _customerService.DeleteCustomerAsync(customerId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
