using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Rumox.API.Tests.Catalogo.Setup;
using Rumox.API.Tests.CRM.Setup;
using System;
using System.Net.Http;
using Xunit;

namespace Rumox.API.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>>
    { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly RumoxAppFactory<TStartup> Factory;
        public HttpClient Client;

        private readonly MySQLSetup Catalogo;
        private readonly MongoSetup CRM;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:5001")
            };

            Factory = new RumoxAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);

            var configuration = (IConfiguration)Factory.Services.GetService(typeof(IConfiguration));

            // TODO: Refatorar este acoplamento
            Catalogo = new MySQLSetup(configuration);
            CRM = new MongoSetup(configuration);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
            Catalogo.Dispose();
            CRM.Dispose();
        }
    }
}
