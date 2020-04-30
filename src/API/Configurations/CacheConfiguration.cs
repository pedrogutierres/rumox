using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Rumox.API.Extensions
{
    public static class CacheConfiguration
    {
        public static void AddCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetRedisConnectionString();
            });
        }

        public static string GetRedisConnectionString(this IConfiguration configuration)
        {
            return configuration?.GetConnectionString("Redis");
        }
    }
}
