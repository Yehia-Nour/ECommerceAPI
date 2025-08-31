using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.AddressesDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;

namespace ECommerceAPI.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public AddressService(IUnitOfWork unitOfWork, ICustomerService customerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AddressResponseDTO>> GetAddressByIdAsync(int id)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(id);
            if (address == null)
                return new ApiResponse<AddressResponseDTO>(404, "Address not found");

            var addressResponse = _mapper.Map<AddressResponseDTO>(address);

            return new ApiResponse<AddressResponseDTO>(200, addressResponse, true);
        }

        public async Task<ApiResponse<AddressResponseDTO>> CreateAddressAsync(AddressCreateDTO addressDto, int customerId)
        {
            var customerResponse = await _customerService.GetCustomerByIdAsync(customerId);
            if (customerResponse.Data == null || customerResponse.StatusCode != 200)
                return new ApiResponse<AddressResponseDTO>(404, "Customer not found.");

            var address = _mapper.Map<Address>(addressDto);
            address.CustomerId = customerId;
            await _unitOfWork.Addresses.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();

            var addressResponse = _mapper.Map<AddressResponseDTO>(address);

            return new ApiResponse<AddressResponseDTO>(200, addressResponse, true);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateAddressAsync(AddressUpdateDTO addressDto, int customerId)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(addressDto.AddressId);
            if (address == null ||  address.CustomerId != customerId)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Address not found.");

            _mapper.Map(addressDto, address);

            _unitOfWork.Addresses.Update(address);
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Addresses with Id {addressDto.AddressId} updated successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteAddressAsync(int addressId, int customerId)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(addressId);
            if (address == null || address.CustomerId != customerId)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Address not found.");

            _unitOfWork.Addresses.Delete(address);
            await _unitOfWork.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Address with Id {addressId} deleted successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }

        public async Task<ApiResponse<List<AddressResponseDTO>>> GetAddressesByCustomerAsync(int customerId)
        {
            var customerResponse = await _customerService.GetCustomerByIdAsync(customerId);
            if (customerResponse.Data == null || customerResponse.StatusCode != 200)
                return new ApiResponse<List<AddressResponseDTO>>(404, "Customer not found.");

            var addresses = _unitOfWork.Addresses
                .GetAll()
                .Where(a => a.CustomerId == customerId)
                .ToList();

            var addressResponses = _mapper.Map<List<AddressResponseDTO>>(addresses);

            return new ApiResponse<List<AddressResponseDTO>>(200, addressResponses, true);
        }

    }
}
