using AutoMapper;
using ECommerce.Domain.Models.Basket;
using ECommerce.Domain.Models.Identity;
using ECommerce.Domain.Models.Order;
using ECommerce.Domain.Models.Product;
using ECommerce.Shared.DTOS;
using ECommerce.Shared.DTOS.AuthenticationDto_s;
using ECommerce.Shared.DTOS.BasketDto_s;
using ECommerce.Shared.DTOS.OrderDto_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            #region Product

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, option => option.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureUrlResolver>());


            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();

            #endregion

            #region Basket

            CreateMap<CustomerBasket,BasketDto>().ReverseMap(); 
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            #endregion

            #region Identity

            CreateMap<Address, AddressDto>().ReverseMap();


            #endregion

            #region Order

            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(desc => desc.ProductName, options => options.MapFrom(src => src.Product.ProductName));
                //.ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderPictureUrlReslover>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();

            #endregion
        }



    }
}
