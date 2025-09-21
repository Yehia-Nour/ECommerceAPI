using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.OrderDTOs;

public class OrderCreateDTO
{
    [Required(ErrorMessage = "Billing Address ID is required.")]
    public int BillingAddressId { get; set; }
    [Required(ErrorMessage = "Shipping Address ID is required.")]
    public int ShippingAddressId { get; set; }
    [Required(ErrorMessage = "At least one order item is required.")]
    [MinLength(1, ErrorMessage = "At least one order item is required.")]
    public List<OrderItemCreateDTO> OrderItems { get; set; }
}
