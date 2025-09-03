using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CategoryDTOs;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CategoryResponseDTO>>> GetAllCategoriesAsync();
        Task<ApiResponse<CategoryResponseDTO>> CreateCategoryAsync(CategoryCreateDTO categoryDto);
        Task<ApiResponse<CategoryResponseDTO>> GetCategoryByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCategoryAsync(CategoryUpdateDTO categoryDto);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(int id);
    }
}
