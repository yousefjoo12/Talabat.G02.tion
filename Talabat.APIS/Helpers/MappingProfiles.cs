using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.APIS.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductsDTO>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
            CreateMap<AddressDTO, Address>();
            CreateMap<AddressI, AddressIDto>().ReverseMap();


            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
             .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
             .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
             .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.Product.ProductUrl))
             .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());



        }
    }
}
