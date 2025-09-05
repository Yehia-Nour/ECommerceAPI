using AutoMapper;
using ECommerceAPI.DTOs.ShoppingCartDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartResponseDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
                .ForMember(dest => dest.TotalBasePrice, opt => opt.Ignore())
                .ForMember(dest => dest.TotalDiscount, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    decimal totalBasePrice = 0;
                    decimal totalDiscount = 0;
                    decimal totalAmount = 0;

                    foreach (var item in dest.CartItems)
                    {
                        totalBasePrice += item.UnitPrice * item.Quantity;
                        totalDiscount += item.Discount * item.Quantity;
                        totalAmount += item.TotalPrice;
                    }

                    dest.TotalBasePrice = totalBasePrice;
                    dest.TotalDiscount = totalDiscount;
                    dest.TotalAmount = totalAmount;
                });

            CreateMap<CartItem, CartItemResponseDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
