using ECommerce.Domain.Contracts.Seed;
using ECommerce.Domain.Models.Product;
using ECommerce.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistence.DataSeed
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StoreDbContext _context;

        public DataSeeding(StoreDbContext context)
        {
            _context = context;
        }

        public void DataSeed()
        {
            if(_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }


            #region Seeding Data

            #region Brand
            if (!_context.Brands.Any())
            {
                var BrandsData = File.ReadAllText(@"..\Infrastructure\ECommerce.Presistence\Data\brands.json");
                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (ProductBrands is not null && ProductBrands.Any())
                {
                    _context.Brands.AddRange(ProductBrands);
                }
                _context.SaveChanges();
            }

            #endregion
            #region Types
            if (!_context.Types.Any())
            {
                var TypesData = File.ReadAllText(@"..\Infrastructure\ECommerce.Presistence\Data\types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (ProductTypes is not null && ProductTypes.Any())
                {
                    _context.Types.AddRange(ProductTypes);
                }
                _context.SaveChanges();
            }
            #endregion
            #region Product
            if (!_context.Products.Any())
            {
                var ProductsData = File.ReadAllText(@"..\Infrastructure\ECommerce.Presistence\Data\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products is not null && Products.Any())
                {
                    _context.Products.AddRange(Products);
                }
                _context.SaveChanges();
            } 
            #endregion

            
            #endregion

        }
    }
}
