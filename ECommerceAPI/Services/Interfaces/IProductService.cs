using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.ProductDTOs;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponseDTO>> CreateProductAsync(ProductCreateDTO productDto);
        Task<ApiResponse<ProductResponseDTO>> GetProductByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductAsync(ProductUpdateDTO productDto);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteProductAsync(int id);
        Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsAsync();
        Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsByCategoryAsync(int categoryId);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductStatusAsync(ProductStatusUpdateDTO productStatusUpdateDTO);

    }
}
