using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.ShoppingCartDTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Implementations;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public CartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("GetCart")]
        public async Task<ActionResult<ApiResponse<CartResponseDTO>>> GetCartByCustomerId()
        {
            var customerId = User.GetCustomerId();
            var response = await _shoppingCartService.GetCartByCustomerIdAsync(customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult<ApiResponse<CartResponseDTO>>> AddToCart([FromBody] AddToCartDTO addToCartDTO)
        {
            var customerId = User.GetCustomerId();
            var response = await _shoppingCartService.AddToCartAsync(addToCartDTO, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCartItem")]
        public async Task<ActionResult<ApiResponse<CartResponseDTO>>> UpdateCartItem([FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            var customerId = User.GetCustomerId();
            var response = await _shoppingCartService.UpdateCartItemAsync(updateCartItemDTO, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("RemoveCartItem/{cartItemId}")]
        public async Task<ActionResult<ApiResponse<CartResponseDTO>>> RemoveCartItem(int cartItemId)
        {
            var customerId = User.GetCustomerId();
            var response = await _shoppingCartService.RemoveCartItemAsync(cartItemId, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("ClearCart")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ClearCart()
        {
            var customerId = User.GetCustomerId();
            var response = await _shoppingCartService.ClearCartAsync(customerId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
