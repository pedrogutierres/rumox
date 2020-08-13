using Core.Infra.Log.ELK.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Rumox.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    // Retornar caso o log estiver desabilitado
                    if (bool.TryParse(context.Configuration["Logging:EnterpriseLog:Disabled"] ?? "false", out var disabled) && disabled)
                        return;

                    logging.AddEnterpriseLogger(context.Configuration, options => context.Configuration.GetSection("Logging:EnterpriseLog").Bind(options));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
