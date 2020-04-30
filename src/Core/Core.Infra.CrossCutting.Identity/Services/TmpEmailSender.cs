using Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Core.Infra.CrossCutting.Identity.Services
{
    public class TmpEmailSender : IEmailSender
    {
        private readonly ILogger<TmpEmailSender> _logger;

        public TmpEmailSender(ILogger<TmpEmailSender> logger)
        {
            _logger = logger;
        }

        public Task EnviarEmailAsync(string email, string assunto, string conteudo)
        {
            _logger.LogInformation($"e-mail: {email} | assunto: {assunto} | conteudo: {conteudo}");
            return Task.CompletedTask;
        }
    }
}
