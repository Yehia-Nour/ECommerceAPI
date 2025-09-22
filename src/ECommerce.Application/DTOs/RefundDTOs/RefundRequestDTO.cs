using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.RefundDTOs;

public class RefundRequestDTO
{
    [Required(ErrorMessage = "Cancellation ID is required.")]
    public int CancellationId { get; set; }
    [Required(ErrorMessage = "Refund Method is required.")]
    public RefundMethod RefundMethod { get; set; }
    [StringLength(500, ErrorMessage = "Refund Reason cannot exceed 500 characters.")]
    public string? RefundReason { get; set; }
    [Required(ErrorMessage = "ProcessedBy is required.")]
    public int ProcessedBy { get; set; }
}
