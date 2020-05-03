using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rumox.API.JwtToken;
using Rumox.API.ResponseType;
using Rumox.API.Tests.Catalogo.Setup;
using Rumox.API.Tests.CRM.Setup;
using Rumox.API.ViewModels.Clientes;
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

        public string UsuarioToken => UsuarioLogado?.AccessToken;
        public UsuarioLogado UsuarioLogado => lazyUsuarioLogado.Value;
        private Lazy<UsuarioLogado> lazyUsuarioLogado => new Lazy<UsuarioLogado>(() =>
        {
            if (usuarioLogado != null) return usuarioLogado;

            var usuario = new { email = "rumox@rumoh.io", senha = "Rumox@2020" };

            // Act
            var response = Client.PostAsJsonAsync("usuarios/login", usuario).Result;

            // Assert
            var result = JsonConvert.DeserializeObject<ResponseSuccess<AuthToken>>(response.Content.ReadAsStringAsync().Result);
            if (result?.Data?.result == null)
            {
                var cliente = new RegistrarClienteViewModel
                {
                    Nome = "Rumoh",
                    Sobrenome = "IO",
                    Email = usuario.email,
                    CPF = new Faker().Person.Cpf(false),
                    Senha = usuario.senha
                };

                response = Client.PostAsJsonAsync("crm/clientes", cliente).Result;
                result = JsonConvert.DeserializeObject<ResponseSuccess<AuthToken>>(response.Content.ReadAsStringAsync().Result);

                if (result?.Data?.result == null)
                    return null;
            }

            usuarioLogado = new UsuarioLogado(
                result.Data.result.user.id,
                result.Data.result.user.email,
                result.Data.result.access_token,
                result.Data.result.refresh_token,
                usuario.senha);

            return usuarioLogado;
        }, false);
        private UsuarioLogado usuarioLogado;

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
