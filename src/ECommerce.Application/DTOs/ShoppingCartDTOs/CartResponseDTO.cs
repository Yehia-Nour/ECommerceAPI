namespace ECommerce.Application.DTOs.ShoppingCartDTOs;

public class CartResponseDTO
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public bool IsCheckedOut { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal TotalBasePrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CartItemResponseDTO>? CartItems { get; set; }
}
