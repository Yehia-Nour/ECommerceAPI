using ECommerceAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.CancellationDTOs
{
    public class CancellationStatusUpdateDTO
    {
        [Required(ErrorMessage = "Cancellation ID is required.")]
        public int CancellationId { get; set; }
        [Required]
        public CancellationStatus Status { get; set; }
        public int? ProcessedBy { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Cancellation charges must be non-negative.")]
        public decimal? CancellationCharges { get; set; }
        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters.")]
        public string Remarks { get; set; }
    }
}
