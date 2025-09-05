using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.ShoppingCartDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ApiResponse<CartResponseDTO>> GetCartByCustomerIdAsync(int customerId);
        Task<ApiResponse<CartResponseDTO>> AddToCartAsync(AddToCartDTO addToCartDTO, int customerId);
        Task<ApiResponse<CartResponseDTO>> UpdateCartItemAsync(UpdateCartItemDTO updateCartItemDTO, int customerId);
        Task<ApiResponse<CartResponseDTO>> RemoveCartItemAsync(int cartItemId, int customerId);
        Task<ApiResponse<ConfirmationResponseDTO>> ClearCartAsync(int customerId);
    }
}
