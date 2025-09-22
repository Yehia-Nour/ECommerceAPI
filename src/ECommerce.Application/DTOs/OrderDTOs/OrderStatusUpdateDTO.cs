using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.OrderDTOs;

public class OrderStatusUpdateDTO
{
    [Required(ErrorMessage = "OrderId is Required")]
    public int OrderId { get; set; }
    [Required]
    [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid Order Status.")]
    public OrderStatus OrderStatus { get; set; }
}
