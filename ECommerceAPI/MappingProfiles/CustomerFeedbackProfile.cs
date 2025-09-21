using AutoMapper;
using ECommerceAPI.DTOs.FeedbackDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class CustomerFeedbackProfile : Profile
    {
        public CustomerFeedbackProfile()
        {
            CreateMap<Feedback, CustomerFeedback>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"));


        }
    }
}
