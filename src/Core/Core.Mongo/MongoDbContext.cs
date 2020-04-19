using Core.Mongo.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;

namespace Core.Mongo
{
    public abstract class MongoDbContext : IDisposable
    {
        public IMongoDatabase Db { get; protected set; }

        public MongoDbContext(IConfiguration configuration, ILogger<MongoDbContext> logger)
        {
            // Teste para logar as querys executadas no mongo (ainda será refatorado e testado melhor)
            /*var mongoUrl = new MongoUrl(configuration.GetMongoDbConnectionString());
            var settings = new MongoClientSettings()
            {
                Server = mongoUrl.Server,
                ClusterConfigurator = cb =>
                {
                    if (Convert.ToBoolean(configuration.GetSection("Logging")?["LogarDatabase"] ?? "false"))
                    {
                        cb.Subscribe<CommandStartedEvent>(e =>
                        {
                            var set = new MongoDB.Bson.IO.JsonWriterSettings()
                            {
                                Indent = true
                            };

                            logger.LogInformation($"{e.CommandName} - {e.Command.ToJson(set)}");
                        });
                    }
                }
            };
            IMongoClient client = new MongoClient(settings);*/

            var mongoUrl = new MongoUrl(configuration.GetMongoDbConnectionString());
            var dataBasename = mongoUrl.DatabaseName;

            IMongoClient client = new MongoClient(mongoUrl);
            Db = client.GetDatabase(dataBasename);

            client.RegisterDefaultConventions();
        }

        public virtual void Dispose()
        {
            Db = null;
        }
    }
}
