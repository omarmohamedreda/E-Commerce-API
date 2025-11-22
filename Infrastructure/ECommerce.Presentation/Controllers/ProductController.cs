using ECommerce.Abstraction;
using ECommerce.Presentation.Attributes;
using ECommerce.Shared;
using ECommerce.Shared.Common;
using ECommerce.Shared.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]  // BaseURL/api/Product
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {


        // Get All Products
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParameters productQueryParameters)
        {
            var Products = await _serviceManager.ProductServices.GetAllProductsAsync(productQueryParameters);
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
