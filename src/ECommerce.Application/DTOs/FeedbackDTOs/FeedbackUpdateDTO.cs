using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.FeedbackDTOs;

public class FeedbackUpdateDTO
{
    [Required(ErrorMessage = "FeedbackId is required.")]
    public int FeedbackId { get; set; }
    [Required(ErrorMessage = "Rating is required.")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }
    [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
    public string Comment { get; set; }
}
