using Core.Infra.Log.ELK.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Infra.Log.ELK.Web
{
    public static class EnterpriseLogMiddlewareExtensions
    {
        public static void AddEnterpriseLog(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterLogService(configuration);
        }

        public static IApplicationBuilder UseEnterpriseLog(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<EnterpriseLogMiddleware>();

            return app;
        }
    }
}
