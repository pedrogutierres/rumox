using Core.Domain.Extensions;
using Core.Infra.Mongo;
using Core.Infra.MySQL;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Rumox.API.Extensions;
using Rumox.API.ResponseType;
using System;
using System.Linq;

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
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory =
                        (context => new BadRequestObjectResult(new ResponseError
                        {
                            Errors = context.ModelState.Values.SelectMany(p => p.Errors).Select(erro => erro.Exception?.Message ?? erro.ErrorMessage)
                        }));
                });

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                options.AddPolicy("Testing",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                options.AddPolicy("Production",
                    builder =>
                        builder
                        .WithOrigins(configuration["AllowedHosts"].Split(','))
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddGzipCompression();

            services.AddHealthChecks()
                .AddMySql(configuration.GetMySQLDbConnectionString(), name: "MySQL")
                .AddRedis(configuration.GetRedisConnectionString(), name: "Redis")
                .AddMongoDb(configuration.GetMongoDbConnectionString(), name: "MongoDB");

            services.AddHealthChecksUI();

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerPathFeature != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = exceptionHandlerPathFeature.Error?.AgruparTodasAsMensagens() }));
                    }
                });
            });

            app.UseRouting();

            app.UseCors(env.EnvironmentName);

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/status", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(options =>
                {
                    options.UIPath = "/api/healthchecks-ui";
                    options.ResourcesPath = "/api/healthchecks-ui-resources";

                    options.UseRelativeApiPath = false;
                    options.UseRelativeResourcesPath = false;
                    options.UseRelativeWebhookPath = false;
                });
            });

            return app;
        }
    }
}
