using ECommerce.Shared;
using ECommerce.Shared.Common;
using ECommerce.Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction
{
    public interface IProductServices
    {
        // Get All Product 
        Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParameters productQueryParameters);

        // Get Product By Id
        Task<ProductDto> GetProductByIdAsync(int id);

        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();


        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
