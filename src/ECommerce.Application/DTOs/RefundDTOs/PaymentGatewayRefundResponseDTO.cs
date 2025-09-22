using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.RefundDTOs;

public class PaymentGatewayRefundResponseDTO
{
    public bool IsSuccess { get; set; }
    public RefundStatus Status { get; set; }
    public string TransactionId { get; set; }
}
