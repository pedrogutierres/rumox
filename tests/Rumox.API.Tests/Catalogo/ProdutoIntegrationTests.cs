using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Rumox.API.ResponseType;
using Rumox.API.Tests.Catalogo.Fixtures;
using Rumox.API.Tests.Config;
using Rumox.API.ViewModels.Categorias;
using Rumox.API.ViewModels.Produtos;
using Rumox.API.ViewModelsGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Rumox.API.Tests.Catalogo
{
    [TestCaseOrderer("Rumox.API.Tests.Config.PriorityOrderer", "Rumox.API.Tests")]
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class ProdutoIntegrationTests : IClassFixture<ProdutoTestsFixture>
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        private readonly ProdutoTestsFixture _produtoTestsFixture;

        public ProdutoIntegrationTests(IntegrationTestsFixture<StartupTests> testsFixture, ProdutoTestsFixture produtoTestsFixture)
        {
            _testsFixture = testsFixture;
            _produtoTestsFixture = produtoTestsFixture;
        }

        [Theory(DisplayName = "Registrar produto com sucesso"), TestPriority(11)]
        [Trait("Produto", "Integração")]
        [Repeat(10)]
        public async Task Produto_RegistrarProduto_Sucesso(int iteracao)
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarRegistrarProdutoViewModel(await ObterCategoriaIdRegistrada());

            // Act
            var response = await _testsFixture.Client.PostAsJsonAsync("catalogo/produtos", produto);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }

        [Fact(DisplayName = "Obter produtos com sucesso"), TestPriority(12)]
        [Trait("Produto", "Integração")]
        public async Task Produto_ObterTodas_RetornarComSucesso()
        {
            // Arrange

            // Act
            var response = await _testsFixture.Client.GetAsync("catalogo/produtos");

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            result.Should().HaveCountGreaterThan(0);
        }


        [Fact(DisplayName = "Obter produto por id"), TestPriority(13)]
        [Trait("Produto", "Integração")]
        public async Task Produto_ObterPorId_RetornarComSucesso()
        {
            // Arrange
            var produtoRegistrado = await ObterProdutoRegistrada();

            // Act
            var response = await _testsFixture.Client.GetAsync($"catalogo/produtos/{produtoRegistrado.Id}");

            // Assert
            response.EnsureSuccessStatusCode();

            var produto = JObject.Parse(await response.Content.ReadAsStringAsync());

            Assert.True(produto.IsValid(_produtoTestsFixture.ObterSchemaProdutoViewModel()));
        }

        [Fact(DisplayName = "Atualizar produto com sucesso"), TestPriority(14)]
        [Trait("Produto", "Integração")]
        public async Task Produto_AtualizarProduto_Sucesso()
        {
            // Arrange
            var produtoRegistrado = await ObterProdutoRegistrada();
            var produto = _produtoTestsFixture.GerarAtualizarProdutoViewModel(await ObterCategoriaIdRegistrada());

            // Act
            var response = await _testsFixture.Client.PutAsJsonAsync($"catalogo/produtos/{produtoRegistrado.Id}", produto);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }

        [Fact(DisplayName = "Inativar produto com sucesso"), TestPriority(15)]
        [Trait("Produto", "Integração")]
        public async Task Produto_IntivarProduto_Sucesso()
        {
            // Arrange
            var produtoRegistrado = await ObterProdutoRegistrada(SituacaoQueryModel.Ativo);
            var produto = new { ativo = false };

            // Act
            var response = await _testsFixture.Client.PatchAsJsonAsync($"catalogo/produtos/{produtoRegistrado.Id}/situacao", produto);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }

        [Fact(DisplayName = "Ativar produto com sucesso"), TestPriority(16)]
        [Trait("Produto", "Integração")]
        public async Task Produto_AtivarProduto_Sucesso()
        {
            // Arrange
            var produtoRegistrado = await ObterProdutoRegistrada(SituacaoQueryModel.Inativo);
            var produto = new { ativo = true };

            // Act
            var response = await _testsFixture.Client.PatchAsJsonAsync($"catalogo/produtos/{produtoRegistrado.Id}/situacao", produto);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<string>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Id);
        }

        private async Task<ProdutoViewModel> ObterProdutoRegistrada(SituacaoQueryModel situacaoQueryModel = SituacaoQueryModel.Ativo)
        {
            var query = situacaoQueryModel switch
            {
                SituacaoQueryModel.Ativo => "?situacao=1",
                SituacaoQueryModel.Inativo => "?situacao=0",
                _ => "?situacao=-1"
            };

            var responseProdutosRegistradas = await _testsFixture.Client.GetAsync($"catalogo/produtos{query}");
            var produtosRegistradas = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(await responseProdutosRegistradas.Content.ReadAsStringAsync());
            produtosRegistradas.Should().HaveCountGreaterThan(0);
            return produtosRegistradas.FirstOrDefault();
        }

        private async Task<Guid> ObterCategoriaIdRegistrada()
        {
            var responseCategoriasRegistradas = await _testsFixture.Client.GetAsync($"catalogo/categorias");
            var categoriasRegistradas = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(await responseCategoriasRegistradas.Content.ReadAsStringAsync());
            categoriasRegistradas.Should().HaveCountGreaterThan(0);
            return categoriasRegistradas.FirstOrDefault().Id;
        }
    }
}
