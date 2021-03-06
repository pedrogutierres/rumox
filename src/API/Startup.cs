using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rumox.API.Configurations;
using Rumox.API.Extensions;

namespace Rumox.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseApiConfig(env, loggerFactory);

            app.UseSwaggerConfig(env);
        }
    }
}
