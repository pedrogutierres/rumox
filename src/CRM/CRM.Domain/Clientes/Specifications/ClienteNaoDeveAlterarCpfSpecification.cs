using Core.Domain.Validations;
using CRM.Domain.Clientes.Interfaces;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Specifications
{
    public class ClienteNaoDeveAlterarCpfSpecification : DomainSpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteNaoDeveAlterarCpfSpecification(Cliente entidade, IClienteRepository clienteRepository) : base(entidade)
        {
            _clienteRepository = clienteRepository;
        }

        public override async Task<bool> EhValido()
        {
            var clienteOriginal = await _clienteRepository.ObterPorId(Entidade.Id);

            return clienteOriginal?.CPF == Entidade.CPF;
        }
    }
}
