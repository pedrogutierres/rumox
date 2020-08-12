using Core.Domain.Validations;
using CRM.Domain.Clientes.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Specifications
{
    public class ClienteDeveTerCpfUnicoSpecification : DomainSpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteDeveTerCpfUnicoSpecification(Cliente entidade, IClienteRepository clienteRepository) : base(entidade)
        {
            _clienteRepository = clienteRepository;
        }

        public override async Task<bool> EhValido()
        {
            return await Task.FromResult(!_clienteRepository.Buscar(p => p.Id != Entidade.Id && p.CPF.Numero == Entidade.CPF.Numero).Any());
        }
    }
}
