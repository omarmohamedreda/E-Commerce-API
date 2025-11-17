
using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Coustom_Middlewares;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Contracts.Seed;
using ECommerce.Domain.Models.Identity;
using ECommerce.Presistence.Contexts;
using ECommerce.Presistence.DataSeed;
using ECommerce.Presistence.Repository;
using ECommerce.Services.MappingProfiles;
using ECommerce.Services.Services;
using ECommerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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

            #region Database Configurations
            builder.Services.AddDbContext<StoreDbContext>(options =>
                       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<StoreIdentityDbContex>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


            // Redis Configuration
            
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));

            });
            #endregion

            #region Bussines Services
            // Service Manager
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            // UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Basket Repository
            builder.Services.AddScoped<IBasketRepository, BasketRepository>(); 
            #endregion

            //Picture URL Resolver
            builder.Services.AddTransient<PictureUrlResolver>();

            // AutoMapper Configuration
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));

            //Identity Configuration
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContex>();


            //DataSeeding
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();




            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(M => M.Value.Errors.Any())
                    .Select(M => new ValidationError()
                    {
                        FieldName = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                    });

                    var Response = new ValidationErrorToReturn()
                    {
                        ValidationErrors = errors
                    };

                    return new BadRequestObjectResult(Response);

                };
            }); 


            #endregion






            var app = builder.Build();

            #region DataSeeding Service
            var scope = app.Services.CreateScope();
            var ObjectSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjectSeeding.DataSeedAsync();
            ObjectSeeding.IdentityInitializeAsync();
            #endregion



            // Configure Exception Middleware
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
