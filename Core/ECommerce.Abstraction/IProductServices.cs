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
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId);

        // Get Product By Id
        Task<ProductDto> GetProductByIdAsync(int id);

        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();


        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
