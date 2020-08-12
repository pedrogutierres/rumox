using Microsoft.AspNetCore.Hosting;

namespace Rumox.API.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static bool IsDevelopment(this IWebHostEnvironment env) => "Development".Equals(env.EnvironmentName);
        public static bool IsTesting(this IWebHostEnvironment env) => "Testing".Equals(env.EnvironmentName);
        public static bool IsProduction(this IWebHostEnvironment env) => "Production".Equals(env.EnvironmentName);
        public static bool IsStaging(this IWebHostEnvironment env) => "Staging".Equals(env.EnvironmentName);
    }
}
