using ECommerceAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Refund
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Cancellation ID is required.")]
        public int CancellationId { get; set; }
        [ForeignKey("CancellationId")]
        public Cancellation Cancellation { get; set; }
        [Required(ErrorMessage = "Payment ID is required.")]
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        [Range(0.01, 100000.00, ErrorMessage = "Refund amount must be between $0.01 and $100,000.00.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public RefundStatus Status { get; set; }
        [Required]
        public string RefundMethod { get; set; }
        [StringLength(500, ErrorMessage = "Refund Reason cannot exceed 500 characters.")]
        public string? RefundReason { get; set; }
        [StringLength(100, ErrorMessage = "Transaction ID cannot exceed 100 characters.")]
        public string? TransactionId { get; set; }
        [Required]
        public DateTime InitiatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? ProcessedBy { get; set; }
    }
}
