using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace NextTech.Products.Infrastructure.EF.Context
{
    public class ProductDBContextFactory : IDesignTimeDbContextFactory<ProductDBContext>
    {
        public ProductDBContext CreateDbContext(params string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductDBContext>();

            if (optionsBuilder.IsConfigured)
                return new ProductDBContext(optionsBuilder.Options);

            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

            var connectionString =
                new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables()
                    .Build()
                    .GetConnectionString("ProductDB");

            optionsBuilder.UseNpgsql(connectionString);

            return new ProductDBContext(optionsBuilder.Options);
        }

        public static ProductDBContext Create()
            => new ProductDBContextFactory().CreateDbContext();
    }
}
