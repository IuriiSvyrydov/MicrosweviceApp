using AutoMapper;
using Microservices.ShoppingCartAPI.Dto;
using Microservices.ShoppingCartAPI.Models;

namespace Microservices.ShoppingCartAPI.Mapping
{
    public class ShoppingCartProfile: Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails,CartDetailsDto>().ReverseMap();
            CreateMap<Cart, CartDto>();

        }
    }
}
