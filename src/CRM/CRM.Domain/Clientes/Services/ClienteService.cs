using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Domain.Services;
using CRM.Domain.Clientes.Events;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.Validations;
using CRM.Domain.Clientes.ValuesObjects;
using CRM.Domain.Interfaces;
using CRM.Events.Clientes;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Services
{
    public class ClienteService : DomainService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(
            IClienteRepository clienteRepository,
            IUnitOfWorkCRM uow, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) 
            : base(uow, mediator, notifications)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Registrar(Cliente cliente)
        {
            if (!ClienteValido(cliente))
                return;

            var validation = new ClienteAptoParaRegistrarValidation(cliente, _clienteRepository).Validate(cliente);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _clienteRepository.Registrar(cliente);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ClienteAdapter.ToClienteRegistradoEvent(cliente));
        }

        public async Task Atualizar(Cliente cliente)
        {
            if (!ClienteValido(cliente))
                return;

            var validation = new ClienteAptoParaAtualizarValidation(cliente, _clienteRepository).Validate(cliente);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _clienteRepository.Atualizar(cliente);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ClienteAdapter.ToClienteAtualizadoEvent(cliente));
        }

        public async Task AlterarEmail(Guid id, string novoEmail)
        {
            var cliente = await ObterCliente(id, "AlterarEmail");
            if (cliente == null) return;

            cliente.AlterarEmail(novoEmail);

            if (!ClienteValido(cliente))
                return;

            var validation = new ClienteAptoParaAtualizarValidation(cliente, _clienteRepository).Validate(cliente);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _clienteRepository.Atualizar(cliente);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ClienteAdapter.ToClienteEmailAlteradoEvent(cliente));
        }

        public async Task AlterarSenha(Guid id, string senhaAtual, string novaSenha, bool ignorarSenhaAtual = false)
        {
            var cliente = await ObterCliente(id, "AlterarSenha");
            if (cliente == null) return;

            if (!ignorarSenhaAtual)
            {
                if (!cliente.Senha.Equals(ClienteSenha.Factory.NovaSenha(senhaAtual, cliente.DataHoraCriacao)))
                {
                    NotificarErro("AlterarSenha", "A senha atual do cliente está incorreta.");
                    return;
                }
            }

            cliente.AlterarSenha(ClienteSenha.Factory.NovaSenha(novaSenha, cliente.DataHoraCriacao));

            if (!ClienteValido(cliente))
                return;

            var validation = new ClienteAptoParaAtualizarValidation(cliente, _clienteRepository).Validate(cliente);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _clienteRepository.Atualizar(cliente);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(new ClienteSenhaAlteradaEvent(cliente.Id));
        }

        public async Task CancelarConta(Guid id, string senha)
        {
            var cliente = await ObterCliente(id, "CancelarConta");
            if (cliente == null) return;

            if (!cliente.Senha.Equals(ClienteSenha.Factory.NovaSenha(senha, cliente.DataHoraCriacao)))
            {
                NotificarErro("CancelarConta", "A senha do cliente está incorreta.");
                return;
            }

            cliente.CancelarConta();

            await _clienteRepository.Atualizar(cliente);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ClienteAdapter.ToClienteContaCanceladaEvent(cliente));
        }

        private async Task<bool> ClienteExistente(Guid id)
        {
            return await _clienteRepository.ExistePorId(id);
        }

        private async Task<Cliente> ObterCliente(Guid id, string messageType)
        {
            var cliente = await _clienteRepository.ObterPorId(id);

            if (cliente != null) return cliente;

            NotificarErro(messageType, "Cliente não encontrado.");
            return null;
        }

        private bool ClienteValido(Cliente cliente)
        {
            if (cliente) return true;

            NotificarValidacoesErro(cliente.ValidationResult);
            return false;
        }
    }
}
