using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.AddressesDTOs;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IAddressService
    {
        Task<ApiResponse<AddressResponseDTO>> CreateAddressAsync(AddressCreateDTO addressDto, int customerId);
        Task<ApiResponse<AddressResponseDTO>> GetAddressByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateAddressAsync(AddressUpdateDTO addressDto, int customerId);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteAddressAsync(int addressId, int customerId);
        Task<ApiResponse<List<AddressResponseDTO>>> GetAddressesByCustomerAsync(int customerId);
    }
}
