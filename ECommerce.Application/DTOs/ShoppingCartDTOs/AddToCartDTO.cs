using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.ShoppingCartDTOs;

public class AddToCartDTO
{
    [Required(ErrorMessage = "ProductId is required.")]
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
    public int Quantity { get; set; }
}
