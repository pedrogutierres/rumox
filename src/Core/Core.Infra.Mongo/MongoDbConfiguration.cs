using Microsoft.Extensions.Configuration;

namespace Core.Infra.Mongo
{
    public static class MongoDbConfiguration
    {
        public static string GetMongoDbConnectionString(this IConfiguration configuration)
        {
            return configuration?.GetConnectionString("MongoDb");
        }
    }
}
