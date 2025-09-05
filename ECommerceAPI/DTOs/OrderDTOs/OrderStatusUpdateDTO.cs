using ECommerceAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.OrderDTOs
{
    public class OrderStatusUpdateDTO
    {
        [Required(ErrorMessage = "OrderId is Required")]
        public int OrderId { get; set; }
        [Required]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid Order Status.")]
        public OrderStatus OrderStatus { get; set; }
    }
}
