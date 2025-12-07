using ECommerce.Domain.Contracts.Seed;
using ECommerce.Domain.Models.Identity;
using ECommerce.Domain.Models.Product;
using ECommerce.Presistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistence.DataSeed
{
    public class DataSeeding(StoreDbContext _context, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, StoreIdentityDbContex _identityDbContex) : IDataSeeding
    {

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

        public async Task IdentityInitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user1 = new ApplicationUser
                    {
                        Email = "Omar@gmail.com",
                        DisplayName = "Omar",
                        PhoneNumber = "01055789631",
                        UserName = "OmarMohamed"

                    };

                    var user2 = new ApplicationUser
                    {
                        Email = "Ali@gmail.com",
                        DisplayName = "Ali",
                        PhoneNumber = "01099789631",
                        UserName = "AliSaad"

                    };

                    await _userManager.CreateAsync(user1, "Pa$$w0rd");
                    await _userManager.CreateAsync(user2, "Pa$$w0rd");

                    await _userManager.AddToRoleAsync(user1, "Admin");
                    await _userManager.AddToRoleAsync(user2, "SuperAdmin");


                }

                await _identityDbContex.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            
        }
    }
}
