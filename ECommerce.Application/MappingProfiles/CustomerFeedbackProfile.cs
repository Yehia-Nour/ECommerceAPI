using AutoMapper;
using ECommerce.Application.DTOs.FeedbackDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.MappingProfiles;

public class CustomerFeedbackProfile : Profile
{
    public CustomerFeedbackProfile()
    {
        CreateMap<Feedback, CustomerFeedback>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"));
    }
}