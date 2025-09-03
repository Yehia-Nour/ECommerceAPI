using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.AddressesDTOs;
using ECommerceAPI.DTOs.CategoryDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories
                .GetAll()
                .Where(c => c.IsActive)
                .ToListAsync();

            var categoryList = _mapper.Map<List<CategoryResponseDTO>>(categories);

            return new ApiResponse<List<CategoryResponseDTO>>(200, categoryList, true);
        }

        public async Task<ApiResponse<CategoryResponseDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null || !category.IsActive)
                return new ApiResponse<CategoryResponseDTO>(404, "Category not found.");

            var categoryResponse = _mapper.Map<CategoryResponseDTO>(category);

            return new ApiResponse<CategoryResponseDTO>(200, categoryResponse, true);
        }

        public async Task<ApiResponse<CategoryResponseDTO>> CreateCategoryAsync(CategoryCreateDTO categoryDto)
        {
            var exists = await _unitOfWork.Categories.CategoryNameExistsAsync(categoryDto.Name);
            if (exists)
                return new ApiResponse<CategoryResponseDTO>(400, "Category name already exists.");

            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            var categoryResponse = _mapper.Map<CategoryResponseDTO>(category);

            return new ApiResponse<CategoryResponseDTO>(200, categoryResponse, true);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCategoryAsync(CategoryUpdateDTO categoryDto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);
            if (category == null || !category.IsActive)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Category not found.");

            if (!string.Equals(category.Name, categoryDto.Name, StringComparison.OrdinalIgnoreCase))
            {
                var exists = await _unitOfWork.Categories.CategoryNameExistsAsync(categoryDto.Name);
                if (exists)
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another category with the same name already exists.");
            }   

            _mapper.Map(categoryDto, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Category with Id {categoryDto.Id} updated successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null || !category.IsActive)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Category not found.");

            category.IsActive = false;
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Category with Id {id} deleted successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }

    }
}
