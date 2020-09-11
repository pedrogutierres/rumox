using AutoMapper;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Domain.ValueObjects;
using CRM.Domain.Clientes;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.ValuesObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rumox.API.JwtToken;
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
    [Authorize]
    public class ClientesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;
        private readonly JwtTokenGenerate _jwtTokenGenerate;

        public ClientesController(
            IMapper mapper,
            IClienteService clienteService,
            IClienteRepository clienteRepository,
            JwtTokenGenerate jwtTokenGenerate,
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMediatorHandler mediator)
            : base(notifications, user, mediator)
        {
            _mapper = mapper;
            _clienteService = clienteService;
            _clienteRepository = clienteRepository;
            _jwtTokenGenerate = jwtTokenGenerate;
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseSuccess<AuthToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarCliente([FromBody]RegistrarClienteViewModel model)
        {
            var cliente = new Cliente(model.Id, new CPF(model.CPF), model.Nome, model.Sobrenome, model.Email, DateTime.UtcNow, ClienteSenha.Factory.NovaSenha(model.Senha));

            await _clienteService.Registrar(cliente);

            if (!OperacaoValida())
                return BadRequest();

            return Response(await _jwtTokenGenerate.GerarToken(cliente));
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarCliente([FromBody]AtualizarClienteViewModel model)
        {
            var cliente = await _clienteRepository.ObterPorId(UsuarioId);
            if (cliente == null)
            {
                NotificarErro("AtualizarCliente", "Cliente não encontrado.");
                return BadRequest();
            }

            cliente.AlterarDadosCadastrais(model.Nome, model.Sobrenome);

            await _clienteService.Atualizar(cliente);

            return Response(cliente.Id);
        }

        [HttpPatch("email")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarEmailCliente([FromBody]AlterarEmailClienteViewModel model)
        {
            await _clienteService.AlterarEmail(UsuarioId, model.Email);

            return Response(UsuarioId);
        }

        // TODO: verificar a possibilidade de refatorar para HttpDelete
        [HttpPatch("cancelar")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarSituacaoCliente([FromBody]CancelarContaClienteViewModel model)
        {
            await _clienteService.CancelarConta(UsuarioId, model.Senha);

            return Response(UsuarioId);
        }
    }
}
