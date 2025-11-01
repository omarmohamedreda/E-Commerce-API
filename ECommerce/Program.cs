
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Contracts.Seed;
using ECommerce.Presistence.Contexts;
using ECommerce.Presistence.DataSeed;
using ECommerce.Presistence.Repository;
using ECommerce.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace ECommerce
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #region Add Configuration DB Service

            builder.Services.AddDbContext<StoreDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //DataSeeding
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            // UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Picture URL Resolver
            builder.Services.AddTransient<PictureUrlResolver>();

            // AutoMapper Configuration
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));


            // Service Manager
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            #endregion





            var app = builder.Build();

            #region Services
            var scope = app.Services.CreateScope();
            var ObjectSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjectSeeding.DataSeedAsync(); 
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
