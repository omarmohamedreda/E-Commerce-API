using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Models.Product;
using ECommerce.Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand>().GetAllAsync();
            // Map Products to ProductDto
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
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
            var Product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            // Map Products to ProductDto
            return _mapper.Map<Product,ProductDto>(Product);
        }
    }
}
