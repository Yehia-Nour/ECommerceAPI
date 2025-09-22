using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.AddressesDTOs;
using ECommerce.Application.DTOsAddressesDTOs;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
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
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
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
        if (address == null || address.CustomerId != customerId)
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
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            return new ApiResponse<List<AddressResponseDTO>>(404, "Customer not found.");

        var addresses = await _unitOfWork.Addresses
            .GetAll()
            .Where(a => a.CustomerId == customerId)
            .ToListAsync();

        var addressList = _mapper.Map<List<AddressResponseDTO>>(addresses);

        return new ApiResponse<List<AddressResponseDTO>>(200, addressList, true);
    }
}