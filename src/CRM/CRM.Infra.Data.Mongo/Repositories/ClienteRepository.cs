using CRM.Domain.Clientes;
using CRM.Domain.Clientes.Interfaces;
using CRM.Infra.Data.Mongo.Context;
using CRM.Infra.Data.Mongo.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace CRM.Infra.Data.Mongo.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(CRMContext context) : base(context)
        {  }

        public Task<Cliente> ObterPorCPF(string cpf)
        {
            return Collection.Find(p => p.CPF.Numero == cpf).FirstOrDefaultAsync();
        }

        public Task<Cliente> ObterPorEmail(string email)
        {
            return Collection.Find(p => p.Email == email).FirstOrDefaultAsync();
        }
    }
}
