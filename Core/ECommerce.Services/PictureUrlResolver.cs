using AutoMapper;
using ECommerce.Domain.Models.Product;
using ECommerce.Shared.DTOS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {

        // http://localhost:5049/api/{sre.. picture]
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            else
            {
                var Url = $"{_configuration.GetSection("URLS")["BaseURL"]}{source.PictureUrl}";
                return Url;
            }

        }
    }
}
