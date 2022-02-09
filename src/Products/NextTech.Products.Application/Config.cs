using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextTech.Products.Application.Product.Dtos;
using System;
using System.Reflection;

namespace NextTech.Products.Application
{
    public static class ApplicationConfig
    {
        public static void AddApplicationModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(ProductDtoMap));
            services.AddProduct();
        }
    }
}
