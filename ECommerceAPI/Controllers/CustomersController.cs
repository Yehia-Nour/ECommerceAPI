using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

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
