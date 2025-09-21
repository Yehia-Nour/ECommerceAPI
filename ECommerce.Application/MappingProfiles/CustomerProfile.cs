using AutoMapper;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerRegistrationDTO, Customer>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerLastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.CustomerEmail))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.CustomerPassword))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.CustomerBirthDate));

        CreateMap<Customer, CustomerResponseDTO>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.CustomerBirthDate, opt => opt.MapFrom(src => src.DateOfBirth));

        CreateMap<CustomerUpdateDTO, Customer>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerLastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.CustomerEmail))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.CustomerBirthDate));


    }
}