using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Models.Product;
using ECommerce.Services.Specifications;
using ECommerce.Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand>().GetAllAsync();
            // Map Products to ProductDto
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId)
        { 
            var Specification = new ProductSpecifications(BrandId,TypeId);   // Where null, Includes Brand and Type
            var Products = await _unitOfWork.GetRepository<Product>().GetAllWihSpecificationsAsync(Specification);
            // Map Products to ProductDto
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType>().GetAllAsync();
            // Map Products to ProductDto
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specification = new ProductSpecifications(id);   // Where id, Includes Brand and Type
            var Product = await _unitOfWork.GetRepository<Product>().GetByIdWihSpecificationsAsync(Specification);
            // Map Products to ProductDto
            return _mapper.Map<Product,ProductDto>(Product);
        }
    }
}
