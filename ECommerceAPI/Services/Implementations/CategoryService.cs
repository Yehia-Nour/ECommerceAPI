using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CategoryDTOs;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        public Task<ApiResponse<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CategoryResponseDTO>> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CategoryResponseDTO>> CreateCategoryAsync(CategoryCreateDTO categoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> UpdateCategoryAsync(CategoryUpdateDTO categoryDto)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
