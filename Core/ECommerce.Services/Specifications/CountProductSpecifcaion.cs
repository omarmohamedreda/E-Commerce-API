using ECommerce.Domain.Models.Product;
using ECommerce.Services.BaseSpecifications;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class CountProductSpecifcaion : BaseSpecifications<Product>
    {
        public CountProductSpecifcaion(ProductQueryParameters queryParameters): 
            base(P => (!queryParameters.BrandId.HasValue || P.BrandId == queryParameters.BrandId) && (!queryParameters.TypeId.HasValue || P.TypeId == queryParameters.TypeId)
            && (string.IsNullOrEmpty(queryParameters.SearchValue) || P.Name.ToLower().Contains(queryParameters.SearchValue.ToLower())))
        {
            // 
        }
    }
}
