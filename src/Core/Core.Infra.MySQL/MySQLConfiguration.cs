using Microsoft.Extensions.Configuration;

namespace Core.Infra.MySQL
{
    public static class MySQLConfiguration
    {
        public static string GetMySQLDbConnectionString(this IConfiguration configuration)
        {
            return configuration?.GetConnectionString("MySQL");
        }
    }
}
