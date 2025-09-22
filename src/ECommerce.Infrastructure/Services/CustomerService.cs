using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork uintOfWork, IMapper mapper)
    {
        _unitOfWork = uintOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (customer is null || !customer.IsActive)
            return new ApiResponse<CustomerResponseDTO>(404, "Customer not found or inactive.");

        var customerResponse = _mapper.Map<CustomerResponseDTO>(customer);

        return new ApiResponse<CustomerResponseDTO>(200, customerResponse, true);
    }

    public async Task<Customer> CreateCustomerAsync(CustomerRegistrationDTO customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);

        customer.Password = BCrypt.Net.BCrypt.HashPassword(customerDto.CustomerPassword);

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();
        return customer;
    }


    public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto, int customerId)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer is null || !customer.IsActive)
            return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found or inactive.");

        _mapper.Map(customerDto, customer);

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.SaveChangesAsync();

        var confirmationMessage = new ConfirmationResponseDTO
        {
            Message = $"Customer with Id {customerId} updated successfully."
        };
        return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
    }

    public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(int customerId)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer is null || !customer.IsActive)
            return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found or inactive.");

        customer.IsActive = false;
        await _unitOfWork.SaveChangesAsync();

        var confirmationMessage = new ConfirmationResponseDTO
        {
            Message = $"Customer with Id {customerId} deleted successfully."
        };
        return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _unitOfWork.Customers.GetCustomerByEmailAsync(email);
    }

    public async Task UpdateCustomerPasswordAsync(Customer customer, string hashedNewPassword)
    {
        customer.Password = BCrypt.Net.BCrypt.HashPassword(hashedNewPassword);
        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.SaveChangesAsync();
    }
}