using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 100 characters.")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
