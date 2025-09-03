using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CategoryDTOs;
using ECommerceAPI.Services.Implementations;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<ApiResponse<List<CategoryResponseDTO>>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDTO>>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDTO>>> CreateCategory([FromBody] CategoryCreateDTO categoryDto)
        {
            var response = await _categoryService.CreateCategoryAsync(categoryDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCategory([FromBody] CategoryUpdateDTO categoryDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(categoryDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
