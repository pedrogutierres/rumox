using Core.Infra.Mongo;
using Core.Infra.MySQL;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rumox.API.Extensions;

namespace Rumox.API.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
               .AddMySql(configuration.GetMySQLDbConnectionString(), name: "MySQL")
               .AddRedis(configuration.GetRedisConnectionString(), name: "Redis")
               .AddMongoDb(configuration.GetMongoDbConnectionString(), name: "MongoDB");

            services.AddHealthChecksUI();
        }

        public static void MapHealthChecksConfiguration(this IEndpointRouteBuilder endpoints)
        {
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
        }
    }
}
