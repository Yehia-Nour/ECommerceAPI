using AutoMapper;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            var map = CreateMap<CustomerRegistrationDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerLastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.CustomerPassword))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.CustomerBirthDate));

            map.ReverseMap()
                .ForMember(dest => dest.CustomerPassword, opt => opt.Ignore());
        }
    }
}
