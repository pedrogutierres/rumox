using System;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Interfaces
{
    public interface IClienteIdentityService
    {
        Task RecuperarSenha(string email);
        Task AlterarSenhaPeloToken(string token, string novaSenha);
    }
}
