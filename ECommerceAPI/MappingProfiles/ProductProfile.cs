using AutoMapper;
using ECommerceAPI.DTOs.ProductDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDTO>();

            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true));

            CreateMap<ProductUpdateDTO, Product>();
        }
    }
}
