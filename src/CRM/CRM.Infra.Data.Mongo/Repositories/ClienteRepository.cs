using CRM.Domain.Clientes;
using CRM.Domain.Clientes.Interfaces;
using CRM.Infra.Data.Mongo.Context;
using CRM.Infra.Data.Mongo.Repository;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace CRM.Infra.Data.Mongo.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(CRMContext context) : base(context)
        {  }

        public async Task<Cliente> ObterPorCPF(string cpf)
        {
            return (await Collection.FindAsync(p => p.CPF.Numero == cpf)).FirstOrDefault();
        }
    }
}
