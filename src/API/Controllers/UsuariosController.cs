using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.ValuesObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rumox.API.JwtToken;
using Rumox.API.ResponseType;
using Rumox.API.ViewModels;
using Rumox.API.ViewModels.Usuarios;
using System;
using System.Threading.Tasks;

namespace Rumox.API.Controllers
{
    [Route("usuarios")]
    public class UsuariosController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IClienteIdentityService _clienteIdentityService;
        private readonly IClienteRepository _clienteRepository;
        private readonly JwtTokenGenerate _jwtTokenGenerate;

        public UsuariosController(
            IClienteService clienteService,
            IClienteIdentityService clienteIdentityService,
            IClienteRepository clienteRepository,
            JwtTokenGenerate jwtTokenGenerate,
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMediatorHandler mediator)
            : base(notifications, user, mediator)
        {
            _clienteService = clienteService;
            _clienteIdentityService = clienteIdentityService;
            _clienteRepository = clienteRepository;
            _jwtTokenGenerate = jwtTokenGenerate;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseSuccess<AuthToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody]UsuarioLoginViewModel model)
        {
            var cliente = await _clienteRepository.ObterPorEmail(model.Email);
            if (cliente == null)
            {
                NotificarErro("Login", "Cliente não encontrado ou senha incorreta.");
                return BadRequest();
            }

            if (!cliente.Senha.Equals(ClienteSenha.Factory.NovaSenha(model.Senha, cliente.DataHoraCriacao)))
            {
                NotificarErro("Login", "Cliente não encontrado ou senha incorreta.");
                return BadRequest();
            }

            return Response(await _jwtTokenGenerate.GerarToken(cliente));
        }

        [HttpPost]
        [Route("login/{id:guid}/refresh-token")]
        [ProducesResponseType(typeof(ResponseSuccess<AuthToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginRefreshToken([FromRoute]Guid id, [FromBody]UsuarioLoginTokenViewModel model)
        {
            try
            {
                await _jwtTokenGenerate.ValidarRefreshTokenAsync(id, model.RefreshToken);
            }
            catch (RefreshTokenException ex)
            {
                NotificarErro("LoginRefreshToken", ex.Message);
                return BadRequest();
            }
            catch
            {
                throw;
            }

            var cliente = await _clienteRepository.ObterPorId(id);

            if (cliente == null)
            {
                NotificarErro("LoginRefreshToken", "Cliente não encontrado.");
                return BadRequest();
            }

            return Response(await _jwtTokenGenerate.GerarToken(cliente));
        }

        [HttpPost]
        [Route("{id:guid}/alterar-senha")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarSenha([FromRoute]Guid id, [FromBody]UsuarioAlterarSenhaViewModel model)
        {
            await _clienteService.AlterarSenha(id, model.SenhaAtual, model.NovaSenha);

            return Response(id);
        }

        [HttpPost]
        [Route("recuperar-senha")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RecuperarSenha([FromBody]UsuarioRecuperarSenhaViewModel model)
        {
            await _clienteIdentityService.RecuperarSenha(model.Email);

            return Response();
        }

        [HttpPost]
        [Route("alterar-senha-por-token")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RecuperarSenha([FromBody]UsuarioAlterarSenhaPorTokenViewModel model)
        {
            await _clienteIdentityService.AlterarSenhaPeloToken(model.Token, model.NovaSenha);

            return Response();
        }
    }
}
