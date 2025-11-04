using ECommerce.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Shared.DTOS;
using ECommerce.Shared.Common;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]  // BaseURL/api/Product
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {


        // Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(int? BrandId, int? TypeId, ProductSortingOptions? sortingOptions)
        {
            var Products = await _serviceManager.ProductServices.GetAllProductsAsync(BrandId, TypeId, sortingOptions);
            return Ok(Products);
        }

        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductServices.GetProductByIdAsync(id);
            return Ok(Product);
        }


        // Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllProductTypes()
        {
            var ProductTypes = await _serviceManager.ProductServices.GetAllTypesAsync();
            return Ok(ProductTypes);


        }

        // Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllProductBrands()
        {
            var ProductBrands = await _serviceManager.ProductServices.GetAllBrandsAsync();
            return Ok(ProductBrands);
        }
    }
}
