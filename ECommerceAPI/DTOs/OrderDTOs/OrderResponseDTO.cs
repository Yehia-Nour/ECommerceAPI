using ECommerceAPI.Models.Enums;

namespace ECommerceAPI.DTOs.OrderDTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public decimal TotalBaseAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; }
    }
}
