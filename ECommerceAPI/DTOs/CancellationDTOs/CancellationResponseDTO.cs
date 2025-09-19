using ECommerceAPI.Models.Enums;

namespace ECommerceAPI.DTOs.CancellationDTOs
{
    public class CancellationResponseDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Reason { get; set; }
        public CancellationStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int? ProcessedBy { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal? CancellationCharges { get; set; }
        public string? Remarks { get; set; }
    }
}
