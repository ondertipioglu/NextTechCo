using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NextTech.Core.MediatR.Queries;
using NextTech.Products.Application.Commands;
using NextTech.Products.Application.Handlers;
using NextTech.Products.Application.Product.Commands;
using NextTech.Products.Application.Product.Dtos;
using NextTech.Products.Application.Product.Handlers;
using NextTech.Products.Application.Product.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application
{
    internal static class ProductConfig
    {
        internal static void AddProduct(this IServiceCollection services)
        {
            AddCommandHandlers(services);
            AddQueryHandlers(services);
            AddEventHandlers(services);
        }
        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateProductCommand, int>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<EnableProductCommand, Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<DisableProductCommand, Unit>, ProductCommandHandler>();
        }
        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetProductByIdQuery, ProductDto>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<SearchProductQuery, SearchProductQueryResponse>, ProductQueryHandler>();
        }
        private static void AddEventHandlers(IServiceCollection services)
        {
        }
    }
}
