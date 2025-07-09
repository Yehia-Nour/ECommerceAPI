using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.CustomerDTOs
{
    public class CustomerUpdateDTO
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.")]
        public DateTime DateOfBirth { get; set; }
    }
}
