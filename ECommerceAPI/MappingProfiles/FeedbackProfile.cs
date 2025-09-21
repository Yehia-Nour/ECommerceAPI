using AutoMapper;
using ECommerceAPI.DTOs.FeedbackDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile() 
        {
            CreateMap<Feedback, FeedbackResponseDTO>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id));

            CreateMap<FeedbackCreateDTO, Feedback>()
                .ForMember(dest=> dest.CreatedAt , opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest=> dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<FeedbackUpdateDTO, Feedback>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
