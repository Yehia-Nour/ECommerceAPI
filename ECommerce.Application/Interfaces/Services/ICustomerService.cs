using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id);
    Task<Customer> CreateCustomerAsync(CustomerRegistrationDTO customerDto);
    Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto, int customerId);
    Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(int customerId);
    Task UpdateCustomerPasswordAsync(Customer customer, string hashedNewPassword);
}