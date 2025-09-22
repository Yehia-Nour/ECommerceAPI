using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Cancellation
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public string Reason { get; set; } = string.Empty;

    public CancellationStatus Status { get; set; }

    public DateTime RequestedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public int? ProcessedBy { get; set; }

    public decimal OrderAmount { get; set; }
    public decimal? CancellationCharges { get; set; } = 0.00m;

    public string? Remarks { get; set; }

    public Refund Refund { get; set; }
}
