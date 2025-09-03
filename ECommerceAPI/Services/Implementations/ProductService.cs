using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.ProductDTOs;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        public Task<ApiResponse<ProductResponseDTO>> CreateProductAsync(ProductCreateDTO productDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ProductResponseDTO>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductAsync(ProductUpdateDTO productDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductStatusAsync(ProductStatusUpdateDTO productStatusUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
