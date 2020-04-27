﻿using AutoMapper;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Domain.ValueObjects;
using CRM.Domain.Clientes;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.ValuesObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rumox.API.ResponseType;
using Rumox.API.ViewModels.Clientes;
using Rumox.API.ViewModelsGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rumox.API.Controllers
{
    [Route("crm/clientes")]
    public class ClientesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(
            IMapper mapper,
            IClienteService clienteService,
            IClienteRepository clienteRepository,
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMediatorHandler mediator)
            : base(notifications, user, mediator)
        {
            _mapper = mapper;
            _clienteService = clienteService;
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClienteViewModel>> ObterClientes([FromQuery]SituacaoQueryModel situacao = SituacaoQueryModel.Ativo)
        {
            var clientes = _clienteRepository.Buscar(p => true);
            clientes = situacao switch
            {
                SituacaoQueryModel.Ativo => clientes.Where(p => p.Ativo),
                SituacaoQueryModel.Inativo => clientes.Where(p => !p.Ativo),
                _ => clientes
            };

            return _mapper.Map<List<ClienteViewModel>>(clientes);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteViewModel>> ObterClientePorId([FromRoute]Guid id)
        {
            var cliente = await _clienteRepository.ObterPorId(id);
            if (cliente == null)
                return NotFound();

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteViewModel>> ObterClientePorCpf([FromRoute]string cpf)
        {
            var cliente = await _clienteRepository.ObterPorCPF(cpf);
            if (cliente == null)
                return NotFound();

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarCliente([FromBody]RegistrarClienteViewModel model)
        {
            var dataHoraCadastro = DateTime.Now;

            var cliente = new Cliente(model.Id, new CPF(model.CPF), model.Nome, model.Sobrenome, model.Email, dataHoraCadastro, ClienteSenha.Factory.NovaSenha(model.Senha, dataHoraCadastro));

            await _clienteService.Registrar(cliente);

            return Response(cliente.Id);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarCliente([FromRoute]Guid id, [FromBody]AtualizarClienteViewModel model)
        {
            var cliente = await _clienteRepository.ObterPorId(id);
            if (cliente == null)
            {
                NotificarErro("AtualizarCliente", "Cliente não encontrado.");
                return BadRequest();
            }

            cliente.AlterarDados(model.Nome, model.Sobrenome);

            await _clienteService.Atualizar(cliente);

            return Response(cliente.Id);
        }

        [HttpPatch("{id:guid}/email")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarEmailCliente([FromRoute]Guid id, [FromBody]AlterarEmailClienteViewModel model)
        {
            await _clienteService.AlterarEmail(id, model.Email);

            return Response(id);
        }

        [HttpDelete("{id:guid}/cancelar")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarSituacaoCliente([FromRoute]Guid id, [FromBody]CancelarContaClienteViewModel model)
        {
            await _clienteService.CancelarConta(id, model.Senha);

            return Response(id);
        }
    }
}