namespace ECommerce.Domain.Entities;

public class Feedback
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
