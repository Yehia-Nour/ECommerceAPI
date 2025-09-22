using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Refund
{
    public int Id { get; set; }

    public int CancellationId { get; set; }
    public Cancellation Cancellation { get; set; }

    public int PaymentId { get; set; }

    public Payment Payment { get; set; }

    public decimal Amount { get; set; }

    public RefundStatus Status { get; set; }

    public string RefundMethod { get; set; }

    public string? RefundReason { get; set; }

    public string? TransactionId { get; set; }

    public DateTime InitiatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int? ProcessedBy { get; set; }
}

