using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Rumox.API.Tests.Config
{
    public class RumoxAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
            builder.UseEnvironment("Testing");
        }
    }
}
