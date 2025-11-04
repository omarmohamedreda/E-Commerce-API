using ECommerce.Domain.Models.Product;
using ECommerce.Services.BaseSpecifications;
using ECommerce.Shared;
using ECommerce.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class ProductSpecifications: BaseSpecifications<Product>
    {
        public ProductSpecifications(ProductQueryParameters productQueryParameters) :base(P=>(!productQueryParameters.BrandId.HasValue || P.BrandId == productQueryParameters.BrandId) && (!productQueryParameters.TypeId.HasValue || P.TypeId == productQueryParameters.TypeId))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch (productQueryParameters.SortingOption) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;


            }
        }

        public ProductSpecifications(int id) : base(p=>p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
