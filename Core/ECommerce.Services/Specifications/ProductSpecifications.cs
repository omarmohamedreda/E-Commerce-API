using ECommerce.Domain.Models.Product;
using ECommerce.Services.BaseSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class ProductSpecifications: BaseSpecifications<Product>
    {
        public ProductSpecifications():base(null)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
