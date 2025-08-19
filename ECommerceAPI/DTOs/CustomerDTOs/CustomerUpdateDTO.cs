using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs.CustomerDTOs
{
    public class CustomerUpdateDTO
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string CustomerPhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.")]
        public DateTime CustomerBirthDate { get; set; }
    }
}
