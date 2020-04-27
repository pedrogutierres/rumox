using Core.Infra.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Rumox.API.Tests.CRM.Setup
{
    // TODO: Refatorar
    public class MongoSetup : IDisposable
    {
        private IMongoDatabase Db;

        public MongoSetup(IConfiguration configuration)
        {
            var mongoUrl = new MongoUrl(configuration.GetMongoDbConnectionString());
            var dataBasename = mongoUrl.DatabaseName;

            IMongoClient client = new MongoClient(mongoUrl);
            Db = client.GetDatabase(dataBasename);

            //client.RegisterDefaultConventions();

            LimparBaseDeDados();
        }

        private void LimparBaseDeDados()
        {
            var collections = Db.ListCollectionNames().ToEnumerable();
            foreach (var collection in collections)
                Db.DropCollection(collection);
        }

        public void Dispose()
        {
            Db = null;
        }
    }
}
