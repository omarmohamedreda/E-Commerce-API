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
        public ProductSpecifications(int? BrandId, int? TypeId) :base(P=>(!BrandId.HasValue || P.BrandId == BrandId) && (!TypeId.HasValue || P.TypeId == TypeId))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }

        public ProductSpecifications(int id) : base(p=>p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
