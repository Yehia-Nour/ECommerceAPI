using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.OrderDTOs
{
    public class OrderItemCreateDTO
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
    }
}
