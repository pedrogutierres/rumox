using Core.Infra.Mongo;
using Core.Infra.MySQL;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Rumox.API.Extensions;

namespace Rumox.API.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheckBuilder = services.AddHealthChecks()
               .AddMySql(configuration.GetMySQLDbConnectionString(), name: "MySQL")
               .AddRedis(configuration.GetRedisConnectionString(), name: "Redis")
               .AddMongoDb(configuration.GetMongoDbConnectionString(), name: "MongoDB");

            if (bool.TryParse(configuration["Logging:EnterpriseLog:Disabled"] ?? "false", out var disabled) && !disabled)
                healthCheckBuilder.AddRabbitMQ(sp => sp.GetRequiredService<ConnectionFactory>(), name: "RabbitMQ Logs");

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
