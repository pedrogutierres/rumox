using System;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Interfaces
{
    public interface IClienteService
    {
        Task Registrar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task AlterarEmail(Guid id, string novoEmail);
        Task AlterarSenha(Guid id, string senhaAtual, string novaSenha, bool ignorarSenhaAtual = false);
        Task CancelarConta(Guid id, string senha);
    }
}
