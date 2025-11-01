
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Contracts.Seed;
using ECommerce.Presistence.Contexts;
using ECommerce.Presistence.DataSeed;
using ECommerce.Presistence.Repository;
using ECommerce.Services;
using Microsoft.EntityFrameworkCore;

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

            #region DataSeeding
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            #endregion

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
