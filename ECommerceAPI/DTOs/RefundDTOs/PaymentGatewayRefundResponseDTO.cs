using ECommerceAPI.Models.Enums;

namespace ECommerceAPI.DTOs.RefundDTOs
{
    public class PaymentGatewayRefundResponseDTO
    {
        public bool IsSuccess { get; set; }
        public RefundStatus Status { get; set; }
        public string TransactionId { get; set; }
    }
}
