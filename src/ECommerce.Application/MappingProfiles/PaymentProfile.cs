using AutoMapper;
using ECommerce.Application.DTOs.PaymentDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.MappingProfiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, PaymentResponseDTO>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id));
    }
}