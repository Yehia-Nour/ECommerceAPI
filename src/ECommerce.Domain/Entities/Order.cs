using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public int BillingAddressId { get; set; }

    public Address BillingAddress { get; set; }

    public int ShippingAddressId { get; set; }

    public Address ShippingAddress { get; set; }

    public decimal TotalBaseAmount { get; set; }

    public decimal TotalDiscountAmount { get; set; }
    public decimal ShippingCost { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

    public Payment Payment { get; set; }

    public Cancellation Cancellation { get; set; }
}
