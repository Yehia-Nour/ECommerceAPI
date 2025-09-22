using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.CustomerDTOs;

public class CustomerResponseDTO
{
    public int CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public DateTime CustomerBirthDate { get; set; }
}
