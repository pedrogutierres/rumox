using Core.Domain.Interfaces;
using CRM.Domain.Clientes;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Interfaces;
using CRM.Events.Clientes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CRM.Domain.Tests.Clientes
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;
        private readonly IClienteService _clienteService;

        public ClienteServiceTests(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;

            _clienteService = _clienteTestsFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com sucesso")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_Adicionar_ExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns(new List<Cliente>().AsQueryable());

            // Act
            _clienteService.Registrar(cliente);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Once);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteRegistradoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com erro")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_Adicionar_ExecutarComErro()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();
            var clienteJaCadastrado = new List<Cliente> { cliente };

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns(clienteJaCadastrado.AsQueryable());

            // Act
            _clienteService.Registrar(cliente);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Never);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteRegistradoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Cliente com sucesso")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_Atualizar_ExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.ObterPorId(cliente.Id)).Returns(Task.FromResult(cliente));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns(new List<Cliente>().AsQueryable());

            // Act
            _clienteService.Atualizar(cliente);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Once);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteAtualizadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Cliente com erro")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_Atualizar_ExecutarComErro()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();
            var clienteJaCadastrado = new List<Cliente> { cliente };

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.ObterPorId(cliente.Id)).Returns(Task.FromResult(_clienteTestsFixture.GerarClienteValido()));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns(clienteJaCadastrado.AsQueryable());

            // Act
            _clienteService.Atualizar(cliente);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Never);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteAtualizadoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Alterar email do Cliente com sucesso")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_AlterarEmail_ExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.ObterPorId(cliente.Id)).Returns(Task.FromResult(cliente));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns(new List<Cliente>().AsQueryable());

            // Act
            _clienteService.Atualizar(cliente);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Once);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteAtualizadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Alterar email do Cliente com erro")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_AlterarEmail_ExecutarComErro()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();
            var clienteJaCadastrado = new List<Cliente> { cliente };

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.ObterPorId(cliente.Id)).Returns(Task.FromResult(_clienteTestsFixture.GerarClienteValido()));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns(clienteJaCadastrado.AsQueryable());

            // Act
            _clienteService.Atualizar(cliente);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Never);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteAtualizadoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }


        [Fact(DisplayName = "Cancelar conta do Cliente com sucesso")]
        [Trait("Cliente", "Serviço de Domínio")]
        public void ClienteService_CancelarConta_ExecutarComSucesso()
        {
            // Arrange
            var senhaPreDefinica = "Rumox123";
            var cliente = _clienteTestsFixture.GerarClienteValidoComSenhaPreDefina(senhaPreDefinica);

            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Setup(p => p.Commit()).Returns(Task.FromResult(true));
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(p => p.ObterPorId(cliente.Id)).Returns(Task.FromResult(cliente));

            // Act
            _clienteService.CancelarConta(cliente.Id, senhaPreDefinica);

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IUnitOfWorkCRM>().Verify(p => p.Commit(), Times.Once);
            _clienteTestsFixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.RaiseEvent(It.IsAny<ClienteContaCanceladaEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(cliente.Ativo);
        }
    }
}
