using Core.Infra.Log.ELK.Models;
using Core.Infra.Log.ELK.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Rumox.API.Configurations
{
    public static class LoggerConfiguration
    {
        public static void AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Retornar caso o log estiver desabilitado
            if (bool.TryParse(configuration["Logging:EnterpriseLog:Disabled"] ?? "false", out var disabled) && disabled)
                return;

            var logConfiguration = new EnterpriseLogOptions();
            configuration.Bind("Logging:EnterpriseLog", logConfiguration);
            services.AddSingleton(logConfiguration);

            services.AddEnterpriseLog(configuration);
        }

        public static void UseLoggingConfiguration(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Retornar caso o log estiver desabilitado
            if (bool.TryParse(app.ApplicationServices.GetRequiredService<IConfiguration>()["Logging:EnterpriseLog:Disabled"] ?? "false", out var disabled) && disabled)
                return;

            app.UseEnterpriseLog(loggerFactory);
        }
    }
}
