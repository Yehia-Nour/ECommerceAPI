using AutoMapper;
using ECommerceAPI.DTOs.CancellationDTOs;
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
        }
    }
}
