using AutoMapper;
using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.MappingProfiles;

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