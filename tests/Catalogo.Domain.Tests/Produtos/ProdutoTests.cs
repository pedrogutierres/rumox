using System;
using Xunit;

namespace Catalogo.Domain.Tests.Produtos
{
    [Collection(nameof(ProdutoCollection))]
    public class ProdutoTests
    {
        private readonly ProdutoTestsFixture _produtoTestsFixture;

        public ProdutoTests(ProdutoTestsFixture produtoTestsFixture)
        {
            _produtoTestsFixture = produtoTestsFixture;
        }

        [Fact(DisplayName = "Instanciar produto válido.")]
        [Trait("Produto", "Domínio")]
        public void Produto_Instanciar_Valido()
        {
            var produto = _produtoTestsFixture.GerarProdutoValido();
            Assert.True(produto.EhValido());
        }

        [Fact(DisplayName = "Instanciar produto inválido.")]
        [Trait("Produto", "Domínio")]
        public void Produto_Instanciar_Invalida()
        {
            var produto = _produtoTestsFixture.GerarProdutoInvalido();
            Assert.False(produto.EhValido());
        }

        [Fact(DisplayName = "Alterar dados do produto com sucesso.")]
        [Trait("Produto", "Domínio")]
        public void Produto_AlterarNome_ComSucesso()
        {
            var produto = _produtoTestsFixture.GerarProdutoValido();

            var novaCategoria = Guid.NewGuid();
            var novaDescricao = "Nova Descrição";
            var novaInformacaoAdicional = "Nova Informação Adicional";
            produto.AlterarCategoria(novaCategoria);
            produto.AlterarDescricaoEInformacoes(novaDescricao, novaInformacaoAdicional);

            Assert.Equal(novaCategoria, produto.CategoriaId);
            Assert.Equal(novaDescricao, produto.Descricao);
            Assert.Equal(novaInformacaoAdicional, produto.InformacoesAdicionais);
        }

        [Fact(DisplayName = "Ativar produto com sucesso.")]
        [Trait("Produto", "Domínio")]
        public void Produto_Ativar_ComSucesso()
        {
            var produto = _produtoTestsFixture.GerarProdutoInativo();
            produto.Ativar();
            Assert.True(produto.Ativo);
        }

        [Fact(DisplayName = "Inativar produto com sucesso.")]
        [Trait("Produto", "Domínio")]
        public void Produto_Inativar_ComSucesso()
        {
            var produto = _produtoTestsFixture.GerarProdutoInativo();
            produto.Inativar();
            Assert.False(produto.Ativo);
        }
    }
}
