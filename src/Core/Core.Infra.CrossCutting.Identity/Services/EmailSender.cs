using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Core.Infra.CrossCutting.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IMediatorHandler _mediator;
        private readonly ILogger<EmailSender> _logger;

        private readonly string _host;
        private readonly string _emailFrom;
        private readonly int _porta;
        private readonly bool _ssl;
        private readonly string _usuario;
        private readonly string _senha;
        private readonly string _nomeExibicao;

        public EmailSender(IMediatorHandler mediator, ILogger<EmailSender> logger, IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;

            try
            {
                _host = configuration.GetSection("EmailConfiguration")?["Host"];
                _emailFrom = configuration.GetSection("EmailConfiguration")?["EmailFrom"];
                _porta = configuration.GetSection("EmailConfiguration")?["Porta"] == "" ? 587 : Convert.ToInt16(configuration.GetSection("EmailConfiguration")?["Porta"]);
                _ssl = Convert.ToBoolean(configuration.GetSection("EmailConfiguration")?["Ssl"] ?? "true");
                _usuario = configuration.GetSection("EmailConfiguration")?["Usuario"];
                _senha = configuration.GetSection("EmailConfiguration")?["Senha"];
                _nomeExibicao = configuration.GetSection("EmailConfiguration")?["NomeExibicao"];
            }
            catch (Exception exception)
            {
                _logger.LogError(1, exception.ToString());
                _mediator.RaiseEvent(new DomainNotification("EmailSender", exception.Message));
            }
        }

        public async Task EnviarEmailAsync(string email, string assunto, string conteudo)
        {
            try
            {
                using (var mensagemDeEmail = new MailMessage())
                {
                    using (var smtpClient = new SmtpClient(_host, _porta))
                    {
                        mensagemDeEmail.From = new MailAddress(_emailFrom, _nomeExibicao);
                        mensagemDeEmail.To.Add(email);
                        mensagemDeEmail.Subject = assunto;
                        mensagemDeEmail.Body = conteudo;
                        mensagemDeEmail.IsBodyHtml = true;

                        smtpClient.Credentials = new NetworkCredential(_usuario, _senha);
                        smtpClient.EnableSsl = _ssl;

                        smtpClient.Send(mensagemDeEmail);
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(1, exception.ToString());
                await _mediator.RaiseEvent(new DomainNotification("EmailSender", exception.Message));
            }
        }
    }
}
