using Core.Domain.Interfaces;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterPorCPF(string cpf);
    }
}
