using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rumox.API.Configurations;
using Rumox.API.Extensions;

namespace Rumox.API
{
    public class StartupTests
    {
        public StartupTests(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfig(Configuration);

            services.AddApiSecurity(Configuration);

            services.AddCacheConfiguration(Configuration);

            services.AddAutoMapperSetup();

            services.AddSwaggerConfig();

            services.AddMediatR(typeof(Startup));

            services.AddDIConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseApiConfig(env, loggerFactory);
        }
    }
}
