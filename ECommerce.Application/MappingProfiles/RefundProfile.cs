using AutoMapper;
using ECommerce.Application.DTOs.RefundDTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.MappingProfiles;

public class RefundProfile : Profile
{
    public RefundProfile()
    {
        CreateMap<Refund, RefundResponseDTO>()
            .ForMember(dest => dest.RefundMethod,
                       opt => opt.MapFrom(src => Enum.Parse<RefundMethod>(src.RefundMethod)));

        CreateMap<RefundRequestDTO, Refund>()
            .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
            .ForMember(dest => dest.Amount, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => RefundStatus.Pending))
            .ForMember(dest => dest.InitiatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<RefundStatusUpdateDTO, Refund>()
            .ForMember(dest => dest.RefundMethod, opt => opt.MapFrom(src => src.RefundMethod.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => RefundStatus.Completed))
            .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .AfterMap((src, dest) =>
            {
                dest.Payment.Status = PaymentStatus.Refunded;
            });
    }
}