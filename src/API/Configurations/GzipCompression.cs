using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Compression;

namespace Rumox.API.Configurations
{
    public static class GzipCompression
    {
        public static void AddGzipCompression(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.Configure<GzipCompressionProviderOptions>(
                options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
        }
    }
}