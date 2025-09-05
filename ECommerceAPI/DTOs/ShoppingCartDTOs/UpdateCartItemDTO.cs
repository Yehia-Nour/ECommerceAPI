using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.ShoppingCartDTOs
{
    public class UpdateCartItemDTO
    {
        [Required(ErrorMessage = "CartItemId is required.")]
        public int CartItemId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
    }
}
