using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Rumox.API.ResponseType;
using Rumox.API.Tests.Catalogo.Fixtures;
using Rumox.API.Tests.Config;
using Rumox.API.ViewModels.Categorias;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Rumox.API.Tests.Catalogo
{
    [TestCaseOrderer("Rumox.API.Tests.Config.PriorityOrderer", "Rumox.API.Tests")]
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class CategoriaIntegrationTests : IClassFixture<CategoriaTestsFixture>
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        private readonly CategoriaTestsFixture _categoriaTestsFixture;

        public CategoriaIntegrationTests(IntegrationTestsFixture<StartupTests> testsFixture, CategoriaTestsFixture categoriaTestsFixture)
        {
            _testsFixture = testsFixture;
            _categoriaTestsFixture = categoriaTestsFixture;
        }

        [Theory(DisplayName = "Registrar categoria com sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração")]
        [Repeat(10)]
        public async Task Categoria_RegistrarCategoria_Sucesso(int iteracao)
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarRegistrarCategoriaViewModel();

            // Act
            var response = await _testsFixture.Client.PostAsJsonAsync("catalogo/categorias", categoria);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }

        [Fact(DisplayName = "Obter categorias com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração")]
        public async Task Categoria_ObterTodas_RetornarComSucesso()
        {
            // Arrange

            // Act
            var response = await _testsFixture.Client.GetAsync("catalogo/categorias");

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            result.Should().HaveCountGreaterThan(0);
        }


        [Fact(DisplayName = "Obter categoria por id"), TestPriority(3)]
        [Trait("Categoria", "Integração")]
        public async Task Categoria_ObterPorId_RetornarComSucesso()
        {
            // Arrange
            var categoriaRegistrada = await ObterCategoriaRegistrada();

            // Act
            var response = await _testsFixture.Client.GetAsync($"catalogo/categorias/{categoriaRegistrada.Id}");

            // Assert
            response.EnsureSuccessStatusCode();

            var categoria = JObject.Parse(await response.Content.ReadAsStringAsync());

            Assert.True(categoria.IsValid(_categoriaTestsFixture.ObterSchemaCategoriaViewModel()));
        }

        [Fact(DisplayName = "Atualizar categoria com sucesso"), TestPriority(4)]
        [Trait("Categoria", "Integração")]
        public async Task Categoria_AtualizarCategoria_Sucesso()
        {
            // Arrange
            var categoriaRegistrada = await ObterCategoriaRegistrada();
            var categoria = _categoriaTestsFixture.GerarAlterarCategoriaViewModel();

            // Act
            var response = await _testsFixture.Client.PutAsJsonAsync($"catalogo/categorias/{categoriaRegistrada.Id}", categoria);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }


        [Fact(DisplayName = "Deletar categoria com sucesso"), TestPriority(5)]
        [Trait("Categoria", "Integração")]
        public async Task Categoria_InativarCategoria_Sucesso()
        {
            // Arrange
            var categoriaRegistrada = await ObterCategoriaRegistrada();

            // Act
            var response = await _testsFixture.Client.DeleteAsync($"catalogo/categorias/{categoriaRegistrada.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        private async Task<CategoriaViewModel> ObterCategoriaRegistrada()
        {
            var responseCategoriasRegistradas = await _testsFixture.Client.GetAsync($"catalogo/categorias");
            var categoriasRegistradas = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(await responseCategoriasRegistradas.Content.ReadAsStringAsync());
            categoriasRegistradas.Should().HaveCountGreaterThan(0);
            return categoriasRegistradas.FirstOrDefault();
        }
    }
}
