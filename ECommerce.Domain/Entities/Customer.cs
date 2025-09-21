namespace ECommerce.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Password { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Address> Addresses { get; set; }

    public ICollection<Order> Orders { get; set; }

    public ICollection<Cart> Carts { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; }
}

