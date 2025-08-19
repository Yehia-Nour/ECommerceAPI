using ECommerceAPI.Data;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerRegistrationDTO customerDto);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto, int customerId);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(int customerId);
        Task UpdateCustomerPasswordAsync(Customer customer, string hashedNewPassword);
    }
}