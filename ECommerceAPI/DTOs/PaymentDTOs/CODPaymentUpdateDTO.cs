using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.PaymentDTOs
{
    public class CODPaymentUpdateDTO
    {
        [Required(ErrorMessage = "Order ID is required.")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Payment Id is required.")]
        public int PaymentId { get; set; }
    }
}
