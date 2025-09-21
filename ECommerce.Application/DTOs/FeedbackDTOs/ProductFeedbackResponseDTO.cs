namespace ECommerce.Application.DTOs.FeedbackDTOs;

public class ProductFeedbackResponseDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double AverageRating { get; set; }
    public List<CustomerFeedback> Feedbacks { get; set; }
}

public class CustomerFeedback
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
