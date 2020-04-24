using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Produtos;
using Catalogo.Domain.Produtos.Interface;
using Catalogo.Events.Produtos;
using Core.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalogo.Domain.Tests.Produtos
{
    [Collection(nameof(ProdutoCollection))]
    public class ProdutoServiceTests
    {
        private readonly ProdutoTestsFixture _produtoTestsFixture;
        private readonly IProdutoService _produtoService;

        public ProdutoServiceTests(ProdutoTestsFixture produtoTestsFixture)
        {
            _produtoTestsFixture = produtoTestsFixture;

            _produtoService = _produtoTestsFixture.ObterProdutoService();
        }

        [Fact(DisplayName = "Adicionar Produto com sucesso")]
        [Trait("Produto", "Serviço de Domínio")]
        public void ProdutoService_Adicionar_ExecutarComSucesso()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();

            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Produto, bool>>>())).Returns(new List<Produto>().AsQueryable());

            // Act
            _produtoService.Registrar(produto);

            // Assert
            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _produtoTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ProdutoRegistradoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Produto com erro")]
        [Trait("Produto", "Serviço de Domínio")]
        public void ProdutoService_Adicionar_ExecutarComErro()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            var produtoJaCadastrado = new List<Produto> { produto };

            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Produto, bool>>>())).Returns(produtoJaCadastrado.AsQueryable());

            // Act
            _produtoService.Registrar(produto);

            // Assert
            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Never);
            _produtoTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ProdutoRegistradoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Produto com sucesso")]
        [Trait("Produto", "Serviço de Domínio")]
        public void ProdutoService_Atualizar_ExecutarComSucesso()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();

            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.ObterPorId(produto.Id)).Returns(Task.FromResult(produto));

            // Act
            _produtoService.Atualizar(produto);

            // Assert
            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _produtoTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ProdutoAtualizadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Produto com erro")]
        [Trait("Produto", "Serviço de Domínio")]
        public void ProdutoService_Atualizar_ExecutarComErro()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            var produtoJaCadastrada = new List<Produto> { produto };

            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.ObterPorId(produto.Id)).Returns(Task.FromResult(_produtoTestsFixture.GerarProdutoValido()));

            // Act
            _produtoService.Atualizar(produto);

            // Assert
            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Never);
            _produtoTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ProdutoAtualizadoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Ativar Produto com sucesso")]
        [Trait("Produto", "Serviço de Domínio")]
        public void ProdutoService_Ativar_ExecutarComSucesso()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoInativo();

            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.ObterPorId(produto.Id)).Returns(Task.FromResult(produto));

            // Act
            _produtoService.Ativar(produto.Id);

            // Assert
            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _produtoTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ProdutoAtivadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(produto.Ativo);
        }

        [Fact(DisplayName = "Inativar Produto com sucesso")]
        [Trait("Produto", "Serviço de Domínio")]
        public void ProdutoService_Inativar_ExecutarComSucesso()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();

            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.ObterPorId(produto.Id)).Returns(Task.FromResult(produto));

            // Act
            _produtoService.Inativar(produto.Id);

            // Assert
            _produtoTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _produtoTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ProdutoInativadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(produto.Ativo);
        }
    }
}
