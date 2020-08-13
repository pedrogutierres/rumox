using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rumox.API.Middlewares;
using Rumox.API.ResponseType;
using System;
using System.Text.Json;

namespace Rumox.API.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory =
                        (context => new BadRequestObjectResult(new ResponseError("Erros de validações encontrados", "Erros nas validações da model enviada", StatusCodes.Status400BadRequest, context.HttpContext.Request.Path, context.ModelState))
                        {
                            ContentTypes = { "application/problem+json" }
                        });
                });

            services.AddCorsConfiguration(configuration);

            services.AddGzipCompression();

            services.AddLoggingConfiguration(configuration);

            services.AddHealthChecksConfiguration(configuration);

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.ConfigureExceptionHandler(env, loggerFactory);

            app.UseLoggingConfiguration(loggerFactory);

            app.UseRouting();

            app.UseCors(env.EnvironmentName);

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksConfiguration();
            });

            return app;
        }
    }
}
