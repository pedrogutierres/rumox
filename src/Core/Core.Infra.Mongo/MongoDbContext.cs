using Core.Infra.Mongo.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;

namespace Core.Infra.Mongo
{
    public abstract class MongoDbContext : IDisposable
    {
        public IMongoDatabase Db { get; protected set; }

        public MongoDbContext(IConfiguration configuration, ILogger<MongoDbContext> logger)
        {
            var mongoUrl = new MongoUrl(configuration.GetMongoDbConnectionString());
            var dataBasename = mongoUrl.DatabaseName;
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);
            mongoClientSettings.ClusterConfigurator = cb =>
            {
                cb.Subscribe<CommandStartedEvent>(e =>
                {
                    // Caso queira identar, decomenctar e colocar dentro do ToJson
                    //var set = new MongoDB.Bson.IO.JsonWriterSettings() { Indent = true };
                    logger.LogInformation($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };

            IMongoClient client = new MongoClient(mongoClientSettings);
            Db = client.GetDatabase(dataBasename);

            client.RegisterDefaultConventions();
        }

        public virtual void Dispose()
        {
            Db = null;
        }
    }
}
