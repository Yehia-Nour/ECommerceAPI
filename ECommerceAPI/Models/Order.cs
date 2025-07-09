using ECommerceAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Order Number cannot exceed 30 characters.")]
        public string OrderNumber { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Billing Address ID is required.")]
        public int BillingAddressId { get; set; }

        [ForeignKey("BillingAddressId")]
        public Address BillingAddress { get; set; }

        [Required(ErrorMessage = "Shipping Address ID is required.")]
        public int ShippingAddressId { get; set; }

        [ForeignKey("ShippingAddressId")]
        public Address ShippingAddress { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 100000.00, ErrorMessage = "Total Base Amount must be between $0.00 and $100,000.00.")]
        public decimal TotalBaseAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 100000.00, ErrorMessage = "Total Discount Amount must be between $0.00 and $100,000.00.")]
        public decimal TotalDiscountAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 10000.00, ErrorMessage = "Shipping Cost must be between $0.00 and $10,000.00.")]
        public decimal ShippingCost { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 110000.00, ErrorMessage = "Total Amount must be between $0.00 and $110,000.00.")]
        public decimal TotalAmount { get; set; }

        [Required]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid Order Status.")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public ICollection<OrderItem> OrderItems { get; set; }

        public Payment Payment { get; set; }

        public Cancellation Cancellation { get; set; }
    }
}
