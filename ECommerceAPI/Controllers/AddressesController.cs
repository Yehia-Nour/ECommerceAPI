using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.AddressesDTOs;
using ECommerce.Application.DTOsAddressesDTOs;
using ECommerce.Application.Helpers;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }


        [HttpPost("CreateAddress")]
        public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> CreateAddress([FromBody] AddressCreateDTO addressDto)
        {
            var customerId = User.GetCustomerId();
            var response = await _addressService.CreateAddressAsync(addressDto, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAddressById/{id}")]
        public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> GetAddressById(int id)
        {
            var response = await _addressService.GetAddressByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateAddress([FromBody] AddressUpdateDTO addressDto)
        {
            var customerId = User.GetCustomerId();
            var response = await _addressService.UpdateAddressAsync(addressDto, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("DeleteAddress/{addressId}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteAddress(int addressId)
        {
            var customerId = User.GetCustomerId();

            var response = await _addressService.DeleteAddressAsync(addressId, customerId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAddressesByCustomer")]
        public async Task<ActionResult<ApiResponse<List<AddressResponseDTO>>>> GetAddressesByCustomer()
        {
            var customerId = User.GetCustomerId();

            var response = await _addressService.GetAddressesByCustomerAsync(customerId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
