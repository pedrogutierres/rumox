using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task EnviarEmailAsync(string email, string assunto, string conteudo);
    }
}
