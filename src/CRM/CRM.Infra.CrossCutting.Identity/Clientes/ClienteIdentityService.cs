using Core.Cache;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using CRM.Domain.Clientes.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CRM.Infra.CrossCutting.Identity.Clientes
{
    public class ClienteIdentityService : IClienteIdentityService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IEmailSender _emailSender;
        private readonly IDistributedCache _cache;
        private readonly IMediatorHandler _mediator;

        private static string CacheKeyRecuperarSenha(string token) => $"cliente:tokenrecuperarsenha:{token}";

        public ClienteIdentityService(
            IClienteRepository clienteRepository,
            IClienteService clienteService,
            IEmailSender emailSender,
            IDistributedCache cache,
            IMediatorHandler mediator)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _emailSender = emailSender;
            _cache = cache;
            _mediator = mediator;
        }

        public async Task RecuperarSenha(string email)
        {
            var cliente = await _clienteRepository.ObterPorEmail(email);
            if (cliente == null)
            {
                await _mediator.RaiseEvent(new DomainNotification("RecuperarSenha", "E-mail não encontrado."));
                return;
            }

            var token = Guid.NewGuid().ToString().Replace("-", string.Empty);

            var recuperarSenhaTokenData = new RecuperarSenhaTokenData(cliente.Id, token);

            var cacheKey = CacheKeyRecuperarSenha(token);

            await _cache.CreateOrUpdateAsync(cacheKey, recuperarSenhaTokenData, minutesExpiration: 60 * 24 * 3); // 3 dias

            await _emailSender.EnviarEmailAsync(email, "Recuperação de senha do Rumox", $"Token: {token}");
        }

        public async Task AlterarSenhaPeloToken(string token, string novaSenha)
        {
            var cacheKey = CacheKeyRecuperarSenha(token);

            string strTokenArmazenado = await _cache.GetStringAsync(cacheKey);
            if (string.IsNullOrWhiteSpace(strTokenArmazenado))
            {
                await _mediator.RaiseEvent(new DomainNotification("AlterarSenhaPeloToken", "Expirado o tempo para recuperar a senha, tente novamente."));
                return;
            }

            var tokenData = JsonConvert.DeserializeObject<RecuperarSenhaTokenData>(strTokenArmazenado);
            if (tokenData == null)
            {
                await _mediator.RaiseEvent(new DomainNotification("AlterarSenhaPeloToken", "Token de recuperação ."));
                return;
            }

            await _cache.RemoveAsync(cacheKey);

            await _clienteService.AlterarSenha(tokenData.ClienteId, "", novaSenha, true);
        }

        private class RecuperarSenhaTokenData
        {
            public Guid ClienteId { get; set; }
            public string Token { get; private set; }

            public RecuperarSenhaTokenData(Guid clienteId, string token)
            {
                ClienteId = clienteId;
                Token = token;
            }
        }
    }
}
