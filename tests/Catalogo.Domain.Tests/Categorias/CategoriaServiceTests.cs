using Catalogo.Domain.Categorias;
using Catalogo.Domain.Categorias.Interfaces;
using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Produtos.Interface;
using Catalogo.Events.Categorias;
using Core.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalogo.Domain.Tests.Categorias
{
    [Collection(nameof(CategoriaCollection))]
    public class CategoriaServiceTests
    {
        private readonly CategoriaTestsFixture _categoriaTestsFixture;
        private readonly ICategoriaService _categoriaService;

        public CategoriaServiceTests(CategoriaTestsFixture categoriaTestsFixture)
        {
            _categoriaTestsFixture = categoriaTestsFixture;

            _categoriaService = _categoriaTestsFixture.ObterCategoriaService();
        }

        [Fact(DisplayName = "Adicionar Categoria com sucesso")]
        [Trait("Categoria", "Serviço de Domínio")]
        public void CategoriaService_Adicionar_ExecutarComSucesso()
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();

            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _categoriaTestsFixture.Mocker.GetMock<ICategoriaRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(new List<Categoria>().AsQueryable());

            // Act
            _categoriaService.Registrar(categoria);

            // Assert
            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _categoriaTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<CategoriaRegistradaEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Categoria com erro")]
        [Trait("Categoria", "Serviço de Domínio")]
        public void CategoriaService_Adicionar_ExecutarComErro()
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();
            var categoriaJaCadastrada = new List<Categoria> { categoria };

            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _categoriaTestsFixture.Mocker.GetMock<ICategoriaRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(categoriaJaCadastrada.AsQueryable());

            // Act
            _categoriaService.Registrar(categoria);

            // Assert
            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Never);
            _categoriaTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<CategoriaRegistradaEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Categoria com sucesso")]
        [Trait("Categoria", "Serviço de Domínio")]
        public void CategoriaService_Atualizar_ExecutarComSucesso()
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();

            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _categoriaTestsFixture.Mocker.GetMock<ICategoriaRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(new List<Categoria>().AsQueryable());

            // Act
            _categoriaService.Atualizar(categoria);

            // Assert
            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _categoriaTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<CategoriaAtualizadaEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Categoria com erro")]
        [Trait("Categoria", "Serviço de Domínio")]
        public void CategoriaService_Atualizar_ExecutarComErro()
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();
            var categoriaJaCadastrada = new List<Categoria> { categoria };

            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _categoriaTestsFixture.Mocker.GetMock<ICategoriaRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(categoriaJaCadastrada.AsQueryable());

            // Act
            _categoriaService.Atualizar(categoria);

            // Assert
            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Never);
            _categoriaTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<CategoriaAtualizadaEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Deletar Categoria com sucesso")]
        [Trait("Categoria", "Serviço de Domínio")]
        public void CategoriaService_Deletar_ExecutarComSucesso()
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();

            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _categoriaTestsFixture.Mocker.GetMock<ICategoriaRepository>().Setup(p => p.ObterPorId(categoria.Id)).Returns(Task.FromResult(categoria));
            
            // Act
            _categoriaService.Deletar(categoria.Id);

            // Assert
            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Once);
            _categoriaTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<CategoriaDeletadaEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Deletar Categoria com erro")]
        [Trait("Categoria", "Serviço de Domínio")]
        public void CategoriaService_Deletar_ExecutarComErro()
        {
            // Arrange
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();
            var categoriaJaCadastrada = new List<Categoria> { categoria };

            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Setup(p => p.Commit()).Returns(true);
            _categoriaTestsFixture.Mocker.GetMock<ICategoriaRepository>().Setup(p => p.ObterPorId(categoria.Id)).Returns(Task.FromResult(categoria));
            _categoriaTestsFixture.Mocker.GetMock<IProdutoRepository>().Setup(p => p.ExistePorCategoria(categoria.Id)).Returns(Task.FromResult(true));

            // Act
            _categoriaService.Deletar(categoria.Id);

            // Assert
            _categoriaTestsFixture.Mocker.GetMock<IUnitOfWorkCatalogo>().Verify(p => p.Commit(), Times.Never);
            _categoriaTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<CategoriaDeletadaEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
