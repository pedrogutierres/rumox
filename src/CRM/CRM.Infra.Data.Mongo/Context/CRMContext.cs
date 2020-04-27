using Core.Infra.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CRM.Infra.Data.Mongo.Context
{
    public class CRMContext : MongoDbContext
    {
        public CRMContext(IConfiguration configuration, ILogger<MongoDbContext> logger) 
            : base(configuration, logger)
        { }
    }
}
