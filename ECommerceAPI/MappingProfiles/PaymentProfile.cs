using AutoMapper;
using ECommerceAPI.DTOs.PaymentDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentResponseDTO>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
