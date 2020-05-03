using FluentAssertions;
using FluentAssertions.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Rumox.API.JwtToken;
using Rumox.API.ResponseType;
using Rumox.API.Tests.Config;
using Rumox.API.Tests.CRM.Fixtures;
using Rumox.API.ViewModels.Categorias;
using Rumox.API.ViewModels.Clientes;
using Rumox.API.ViewModelsGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Rumox.API.Tests.CRM
{
    [TestCaseOrderer("Rumox.API.Tests.Config.PriorityOrderer", "Rumox.API.Tests")]
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class ClienteIntegrationTests : IClassFixture<ClienteTestsFixture>
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteIntegrationTests(IntegrationTestsFixture<StartupTests> testsFixture, ClienteTestsFixture clienteTestsFixture)
        {
            _testsFixture = testsFixture;
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Theory(DisplayName = "Registrar cliente com sucesso"), TestPriority(21)]
        [Trait("Cliente", "Integração")]
        [Repeat(10)]
        public async Task Cliente_RegistrarCliente_Sucesso(int iteracao)
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarRegistrarClienteViewModel(iteracao == 1 ? "Rumox123" : null);

            // Act
            var response = await _testsFixture.Client.PostAsJsonAsync("crm/clientes", cliente);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<AuthToken>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Data.result.user);

            if (iteracao == 1)
                _clienteTestsFixture.RegistrarClienteParaCancelar(result.Data.result.user.id.ToString(), result.Data.result.user.email, "Rumox123");
        }

        [Fact(DisplayName = "Obter clientes com sucesso"), TestPriority(22)]
        [Trait("Cliente", "Integração")]
        public async Task Cliente_ObterTodas_RetornarComSucesso()
        {
            // Arrange

            // Act
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            var response = await _testsFixture.Client.GetAsync("crm/clientes");

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<List<ClienteViewModel>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            result.Should().HaveCountGreaterThan(0);
        }


        [Fact(DisplayName = "Obter cliente por id"), TestPriority(23)]
        [Trait("Cliente", "Integração")]
        public async Task Cliente_ObterPorId_RetornarComSucesso()
        {
            // Arrange
            var clienteRegistrado = await ObterClienteRegistrada();

            // Act
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            var response = await _testsFixture.Client.GetAsync($"crm/clientes/{clienteRegistrado.Id}");

            // Assert
            response.EnsureSuccessStatusCode();

            var cliente = JObject.Parse(await response.Content.ReadAsStringAsync());

            Assert.True(cliente.IsValid(_clienteTestsFixture.ObterSchemaClienteViewModel()));
        }

        [Fact(DisplayName = "Obter cliente por cpf"), TestPriority(24)]
        [Trait("Cliente", "Integração")]
        public async Task Cliente_ObterPorCPF_RetornarComSucesso()
        {
            // Arrange
            var clienteRegistrado = await ObterClienteRegistrada();

            // Act
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            var response = await _testsFixture.Client.GetAsync($"crm/clientes/cpf/{clienteRegistrado.CPF}");

            // Assert
            response.EnsureSuccessStatusCode();

            var cliente = JObject.Parse(await response.Content.ReadAsStringAsync());

            Assert.True(cliente.IsValid(_clienteTestsFixture.ObterSchemaClienteViewModel()));
        }

        [Fact(DisplayName = "Atualizar cliente com sucesso"), TestPriority(25)]
        [Trait("Cliente", "Integração")]
        public async Task Cliente_AtualizarCliente_Sucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarAtualizarClienteViewModel();

            // Act
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            var response = await _testsFixture.Client.PutAsJsonAsync($"crm/clientes", cliente);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }

        [Fact(DisplayName = "Alterar email do cliente com sucesso"), TestPriority(26)]
        [Trait("Cliente", "Integração")]
        public async Task Cliente_AlterarEmailCliente_Sucesso()
        {
            // Arrange
            var cliente = new { email = _clienteTestsFixture.Faker.Person.Email };

            // Act
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            var response = await _testsFixture.Client.PatchAsJsonAsync($"crm/clientes/email", cliente);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);

            _testsFixture.AtualizarDadosUsuarioLogado(email: cliente.email);
        }

        [Fact(DisplayName = "Cancelar conta do cliente com sucesso"), TestPriority(27)]
        [Trait("Cliente", "Integração")]
        public async Task Cliente_CancelarContaCliente_Sucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarCancelarContaClienteViewModel(out var _, out var email);

            var usuarioLogin = new { email, ((dynamic)cliente).senha };
            var responseLogin = _testsFixture.Client.PostAsJsonAsync("usuarios/login", usuarioLogin).Result;
            var resultLogin = JsonConvert.DeserializeObject<ResponseSuccess<AuthToken>>(responseLogin.Content.ReadAsStringAsync().Result);
            Assert.NotNull(resultLogin?.Data?.result);

            // Act
            _testsFixture.Client.AtribuirToken(resultLogin.Data.result.access_token);
            var response = await _testsFixture.Client.PatchAsJsonAsync($"crm/clientes/cancelar", cliente);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        private async Task<ClienteViewModel> ObterClienteRegistrada(SituacaoQueryModel situacaoQueryModel = SituacaoQueryModel.Ativo)
        {
            var query = situacaoQueryModel switch
            {
                SituacaoQueryModel.Ativo => "?situacao=1",
                SituacaoQueryModel.Inativo => "?situacao=0",
                _ => "?situacao=-1"
            };

            var responseClientesRegistradas = await _testsFixture.Client.GetAsync($"crm/clientes{query}");
            var clientesRegistradas = JsonConvert.DeserializeObject<List<ClienteViewModel>>(await responseClientesRegistradas.Content.ReadAsStringAsync());
            clientesRegistradas.Should().HaveCountGreaterThan(0);
            return clientesRegistradas.FirstOrDefault();
        }

        private async Task<Guid> ObterCategoriaIdRegistrada()
        {
            var responseCategoriasRegistradas = await _testsFixture.Client.GetAsync($"crm/categorias");
            var categoriasRegistradas = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(await responseCategoriasRegistradas.Content.ReadAsStringAsync());
            categoriasRegistradas.Should().HaveCountGreaterThan(0);
            return categoriasRegistradas.FirstOrDefault().Id;
        }
    }
}
