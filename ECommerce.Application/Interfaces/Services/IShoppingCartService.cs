using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.ShoppingCartDTOs;

namespace ECommerce.Application.Interfaces.Services;

public interface IShoppingCartService
{
    Task<ApiResponse<CartResponseDTO>> GetCartByCustomerIdAsync(int customerId);
    Task<ApiResponse<CartResponseDTO>> AddToCartAsync(AddToCartDTO addToCartDTO, int customerId);
    Task<ApiResponse<CartResponseDTO>> UpdateCartItemAsync(UpdateCartItemDTO updateCartItemDTO, int customerId);
    Task<ApiResponse<CartResponseDTO>> RemoveCartItemAsync(int cartItemId, int customerId);
    Task<ApiResponse<ConfirmationResponseDTO>> ClearCartAsync(int customerId);
}