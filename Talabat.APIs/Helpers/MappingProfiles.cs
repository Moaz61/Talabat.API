using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Product;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name))
                //.ForMember(d => d.PictureUrl, O => O.MapFrom(S => $"{"https://localhost:7072"}/{S.PictureUrl}"));
                .ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}
