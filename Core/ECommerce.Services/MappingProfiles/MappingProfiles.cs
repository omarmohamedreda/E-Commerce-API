using AutoMapper;
using ECommerce.Domain.Models.Product;
using ECommerce.Services.Services;
using ECommerce.Shared.DTOS;
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
        }



    }
}
