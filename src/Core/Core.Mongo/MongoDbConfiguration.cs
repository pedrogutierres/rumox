using Microsoft.Extensions.Configuration;

namespace Core.Mongo
{
    public static class MongoDbConfiguration
    {
        public static string GetMongoDbConnectionString(this IConfiguration configuration)
        {
            return configuration?.GetConnectionString("MongoDb");
        }
    }
}
