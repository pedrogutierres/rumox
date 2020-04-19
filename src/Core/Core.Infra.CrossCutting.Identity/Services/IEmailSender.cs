using System.Threading.Tasks;

namespace Core.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task EnviarEmailAsync(string email, string assunto, string conteudo);
    }
}
