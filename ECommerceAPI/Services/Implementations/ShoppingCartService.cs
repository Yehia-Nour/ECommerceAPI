using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.ShoppingCartDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ShoppingCartService(IUnitOfWork unitOfWork, IProductService productService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CartResponseDTO>> GetCartByCustomerIdAsync(int customerId)
        {
            var cart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                var emptyCartDTO = new CartResponseDTO
                {
                    CustomerId = customerId,
                    IsCheckedOut = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CartItems = new List<CartItemResponseDTO>(),
                    TotalBasePrice = 0,
                    TotalDiscount = 0,
                    TotalAmount = 0
                };
                return new ApiResponse<CartResponseDTO>(200, emptyCartDTO);
            }
            var cartResponse = _mapper.Map<CartResponseDTO>(cart);
            return new ApiResponse<CartResponseDTO>(200, cartResponse, true);
        }

        public async Task<ApiResponse<CartResponseDTO>> AddToCartAsync(AddToCartDTO addToCartDTO, int customerId)
        {
            var productResponse = await _productService.GetProductByIdAsync(addToCartDTO.ProductId);
            if (!productResponse.Success)
                return new ApiResponse<CartResponseDTO>(404, "Product not found.");

            var product = productResponse.Data;

            if (addToCartDTO.Quantity > product.StockQuantity)
                return new ApiResponse<CartResponseDTO>(400, $"Only {product.StockQuantity} units of {product.Name} are available.");

            var cart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    IsCheckedOut = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CartItems = new List<CartItem>()
                };

                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);

            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity + addToCartDTO.Quantity > product.StockQuantity)
                    return new ApiResponse<CartResponseDTO>(400, $"Adding {addToCartDTO.Quantity} exceeds available stock.");

                existingCartItem.Quantity += addToCartDTO.Quantity;
                existingCartItem.TotalPrice = (existingCartItem.UnitPrice - existingCartItem.Discount) * existingCartItem.Quantity;
                existingCartItem.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.CartItems.Update(existingCartItem);
            }
            else
            {
                var discount = product.DiscountPercentage > 0 ? product.Price * product.DiscountPercentage / 100 : 0;

                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = addToCartDTO.Quantity,
                    UnitPrice = product.Price,
                    Discount = discount,
                    TotalPrice = (product.Price - discount) * addToCartDTO.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _unitOfWork.CartItems.AddAsync(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            var updatedCart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            var cartResponse = _mapper.Map<CartResponseDTO>(updatedCart);

            return new ApiResponse<CartResponseDTO>(200, cartResponse);
        }

        public async Task<ApiResponse<CartResponseDTO>> UpdateCartItemAsync(UpdateCartItemDTO updateCartItemDTO, int customerId)
        {
            var cart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
                return new ApiResponse<CartResponseDTO>(404, "Active cart not found.");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == updateCartItemDTO.CartItemId);
            if (cartItem == null)
                return new ApiResponse<CartResponseDTO>(404, "Cart item not found.");

            if (updateCartItemDTO.Quantity > cartItem.Product.StockQuantity)
                return new ApiResponse<CartResponseDTO>(400, $"Only {cartItem.Product.StockQuantity} units of {cartItem.Product.Name} are available.");


            cartItem.Quantity = updateCartItemDTO.Quantity;
            cartItem.TotalPrice = (cartItem.UnitPrice - cartItem.Discount) * cartItem.Quantity;
            cartItem.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.CartItems.Update(cartItem);

            cart.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            var updatedCart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            var cartResponse = _mapper.Map<CartResponseDTO>(updatedCart);

            return new ApiResponse<CartResponseDTO>(200, cartResponse);
        }

        public async Task<ApiResponse<CartResponseDTO>> RemoveCartItemAsync(int cartItemId, int customerId)
        {
            var cart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
                return new ApiResponse<CartResponseDTO>(404, "Active cart not found.");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
                return new ApiResponse<CartResponseDTO>(404, "Cart item not found.");

            _unitOfWork.CartItems.Delete(cartItem);
            cart.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            var updatedCart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            var cartResponse = _mapper.Map<CartResponseDTO>(updatedCart);

            return new ApiResponse<CartResponseDTO>(200, cartResponse);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> ClearCartAsync(int customerId)
        {
            var cart = await _unitOfWork.Carts.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Active cart not found.");

            if (cart.CartItems.Any())
            {
                _unitOfWork.CartItems.RemoveRange(cart.CartItems);
                cart.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Carts.Update(cart);

                await _unitOfWork.SaveChangesAsync();
            }

            var confirmation = new ConfirmationResponseDTO
            {
                Message = "Cart has been cleared successfully."
            };

            return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
        }

    }
}
