using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NextTech.Core.Exceptions;
using NextTech.Core.MediatR.Config;
using NextTech.Core.WebApi.Extensions;
using NextTech.Core.WebApi.Middlewares.ExceptionHandling;
using NextTech.Products.Application;
using NextTech.Products.Infrastructure.EF.Config;
using NextTech.Products.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace NextTech.Products.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NextTech.Product.Api", Version = "v1" });
            });
            services.AddMediatRServices();
            services.AddApplicationModule(Configuration);
            services.AddEFModule(Configuration);
            services.AddApiVersioningExtension();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NextTech.Product.Api v1"));
            }

            app.UseExceptionHandlingMiddleware(exception => exception switch
            {
                //AggregateNotFoundException _ => HttpStatusCode.NotFound,
                //AlreadyExistsException _ => HttpStatusCode.Conflict,
                NotFoundException _ => HttpStatusCode.NotFound,
                AggregateNotFoundException _ => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            });

            app.UseDataInitializer();
            app.UseRequestLogMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
