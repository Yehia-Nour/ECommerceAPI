using AutoMapper;
using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderItem, OrderItemResponseDTO>();

        CreateMap<Order, OrderResponseDTO>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => Math.Round(src.TotalAmount, 2)))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));
    }
}