using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextTech.Products.Application;
using NextTech.Products.Application.Product;
using NextTech.Products.Infrastructure.EF.Context;
using NextTech.Products.Infrastructure.EF.Repositories;
using System.Threading.Tasks;

namespace NextTech.Products.Infrastructure.EF.Config
{
    public static class EFConfig
    {
        public static IServiceCollection AddEFModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddEFDataService<ProductDBContext>(configuration);
        }
        private static IServiceCollection AddEFDataService<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("ProductDB");
            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
            });
            services.AddScoped<DbContext, TContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<DataMigrator>();      

            return services;
        }

        public static async Task UseDataInitializer(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ProductDBContext>();
                dbContext.Database.Migrate();

                var initializer = scope.ServiceProvider.GetService<DataMigrator>();                
                await initializer.Seed();
            }
        }
    } 
}
