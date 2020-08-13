using Core.Infra.Log.ELK.Models;
using Core.Infra.Log.ELK.Services;
using Core.Infra.Log.ELK.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Core.Infra.Log.ELK.Logging
{
    public static class EnterpriseLoggerExtensions
    {
        public static ILoggingBuilder AddEnterpriseLogger(this ILoggingBuilder builder, IConfiguration configuration, Action<EnterpriseLogOptions> configure)
        {
            builder.Services.RegisterLogService(configuration);

            builder.Services.AddSingleton<ILoggerProvider, EnterpriseLoggerProvider>();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
