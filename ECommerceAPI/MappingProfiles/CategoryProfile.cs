using AutoMapper;
using ECommerceAPI.DTOs.CategoryDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponseDTO>();

            CreateMap<CategoryCreateDTO, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CategoryUpdateDTO, Category>();
        }
    }
}
