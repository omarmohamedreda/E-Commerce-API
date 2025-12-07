using AutoMapper;
using ECommerce.Domain.Models.Order;
using ECommerce.Shared.DTOS.OrderDto_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class OrderPictureUrlReslover(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            else
            {
                var Url = $"{_configuration.GetSection("URLS")["BaseURL"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }
}
