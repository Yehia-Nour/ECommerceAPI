using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.ProductDTOs;

public class ProductUpdateDTO
{
    [Required(ErrorMessage = "Product Id is required.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Product Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Product Name must be between 3 and 100 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    [MinLength(10, ErrorMessage = "Description must be at least 10 characters.")]
    public string Description { get; set; }
    [Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.00.")]
    public decimal Price { get; set; }
    [Range(0, 1000, ErrorMessage = "Stock Quantity must be between 0 and 1000.")]
    public int StockQuantity { get; set; }
    [Url(ErrorMessage = "Invalid Image URL.")]
    public string ImageUrl { get; set; }
    [Range(0, 100, ErrorMessage = "Discount Percentage must be between 0% and 100%.")]
    public int DiscountPercentage { get; set; }
    [Required(ErrorMessage = "Category ID is required.")]
    public int CategoryId { get; set; }
}
