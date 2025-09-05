using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CategoryDTOs;
using ECommerceAPI.DTOs.ProductDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, ICategoryService categoryService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products
                .GetAll()
                .Where(p => p.IsAvailable)
                .ToListAsync();

            var productResponses = _mapper.Map<List<ProductResponseDTO>>(products);

            return new ApiResponse<List<ProductResponseDTO>>(200, productResponses, true);
        }

        public async Task<ApiResponse<ProductResponseDTO>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null || !product.IsAvailable)
                return new ApiResponse<ProductResponseDTO>(404, "Product not found.");

            var productResponse = _mapper.Map<ProductResponseDTO>(product);

            return new ApiResponse<ProductResponseDTO>(200, productResponse);
        }

        public async Task<ApiResponse<ProductResponseDTO>> CreateProductAsync(ProductCreateDTO productDto)
        {
            var exists = await _unitOfWork.Products.ProductNameExistsAsync(productDto.Name);
            if (exists)
                return new ApiResponse<ProductResponseDTO>(400, "Product name already exists.");

            var categoryResponse = await _categoryService.GetCategoryByIdAsync(productDto.CategoryId);
            if (!categoryResponse.Success)
                return new ApiResponse<ProductResponseDTO>(400, "Specified category does not exist.");

            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var productResponse = _mapper.Map<ProductResponseDTO>(product);

            return new ApiResponse<ProductResponseDTO>(200, productResponse, true);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductAsync(ProductUpdateDTO productDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productDto.Id);
            if (product == null || !product.IsAvailable)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");

            if (!string.Equals(product.Name, productDto.Name, StringComparison.OrdinalIgnoreCase))
            {
                var exists = await _unitOfWork.Products.ProductNameExistsAsync(productDto.Name);
                if (exists)
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another product with the same name already exists.");
            }

            var categoryResponse = await _categoryService.GetCategoryByIdAsync(productDto.CategoryId);
            if (!categoryResponse.Success)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Specified category does not exist.");

            _mapper.Map(productDto, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Product with Id {productDto.Id} updated successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null || !product.IsAvailable)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");

            product.IsAvailable = false;
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Product with Id {id} deleted successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }

        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetAll()
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsAvailable)
                .ToListAsync();

            if (products == null || products.Count == 0)
                return new ApiResponse<List<ProductResponseDTO>>(200, new List<ProductResponseDTO>(), true);

            var productList = _mapper.Map<List<ProductResponseDTO>>(products);
            return new ApiResponse<List<ProductResponseDTO>>(200, productList);
        }


        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductStatusAsync(ProductStatusUpdateDTO productStatusUpdateDTO)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productStatusUpdateDTO.ProductId);
            if (product == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");

            product.IsAvailable = productStatusUpdateDTO.IsAvailable;
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Product with Id {productStatusUpdateDTO.ProductId} Status Updated successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }
    }
}
