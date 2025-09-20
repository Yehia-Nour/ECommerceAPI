using AutoMapper;
using ECommerceAPI.DTOs.CancellationDTOs;
using ECommerceAPI.DTOs.RefundDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class CancellationProfile : Profile
    {
        public CancellationProfile()
        {
            CreateMap<Cancellation, CancellationResponseDTO>()
                .ForMember(dest => dest.OrderAmount, opt => opt.MapFrom((src, dest, _, context) =>
                    context.Items["OrderTotalAmount"] != null ? (decimal)context.Items["OrderTotalAmount"] : src.OrderAmount));

            CreateMap<Cancellation, PendingRefundResponseDTO>()
                .ForMember(dest => dest.CancellationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.OrderAmount, opt => opt.MapFrom(src => src.OrderAmount))
                .ForMember(dest => dest.CancellationCharge, opt => opt.MapFrom(src => src.CancellationCharges ?? 0.00m))
                .ForMember(dest => dest.ComputedRefundAmount, opt => opt.MapFrom(src => src.OrderAmount - (src.CancellationCharges ?? 0.00m)))
                .ForMember(dest => dest.CancellationRemarks, opt => opt.MapFrom(src => src.Remarks));
        }
    }
}
