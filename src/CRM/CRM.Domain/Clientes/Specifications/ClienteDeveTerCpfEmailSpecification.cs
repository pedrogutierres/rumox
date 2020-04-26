using Core.Domain.Validations;
using CRM.Domain.Clientes.Interfaces;
using System.Linq;

namespace CRM.Domain.Clientes.Specifications
{
    public class ClienteDeveTerCpfEmailSpecification : DomainSpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteDeveTerCpfEmailSpecification(Cliente entidade, IClienteRepository clienteRepository) : base(entidade)
        {
            _clienteRepository = clienteRepository;
        }

        public override bool EhValido()
        {
            return !_clienteRepository.Buscar(p => p.Id != Entidade.Id && p.Email == Entidade.Email).Any();
        }
    }
}
