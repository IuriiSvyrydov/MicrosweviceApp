using AutoMapper;
using MicroservicesApp.Product.API.Dtos;

namespace MicroservicesApp.Product.API.Mapping
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, ProductDto>().ReverseMap();
        }
    }
}
