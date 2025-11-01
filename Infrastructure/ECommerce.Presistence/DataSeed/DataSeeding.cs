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

        public async Task DataSeedAsync()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                _context.Database.Migrate();
            }


            #region Seeding Data

            #region Brand
            if (!_context.Brands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\ECommerce.Presistence\Data\brands.json");
                var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);

                if (ProductBrands is not null && ProductBrands.Any())
                {
                    await _context.Brands.AddRangeAsync(ProductBrands);
                }
                await _context.SaveChangesAsync();
            }

            #endregion
            #region Types
            if (!_context.Types.Any())
            {
                var TypesData = File.OpenRead(@"..\Infrastructure\ECommerce.Presistence\Data\types.json");
                var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);

                if (ProductTypes is not null && ProductTypes.Any())
                {
                    await _context.Types.AddRangeAsync(ProductTypes);
                }
                await _context.SaveChangesAsync();
            }
            #endregion
            #region Product
            if (!_context.Products.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\ECommerce.Presistence\Data\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);

                if (Products is not null && Products.Any())
                {
                    await _context.Products.AddRangeAsync(Products);
                }
                await _context.SaveChangesAsync();
            } 
            #endregion

            
            #endregion

        }
    }
}
