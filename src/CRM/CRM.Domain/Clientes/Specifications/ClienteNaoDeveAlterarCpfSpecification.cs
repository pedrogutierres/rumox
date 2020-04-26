using Core.Domain.Validations;
using CRM.Domain.Clientes.Interfaces;

namespace CRM.Domain.Clientes.Specifications
{
    public class ClienteNaoDeveAlterarCpfSpecification : DomainSpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteNaoDeveAlterarCpfSpecification(Cliente entidade, IClienteRepository clienteRepository) : base(entidade)
        {
            _clienteRepository = clienteRepository;
        }

        public override bool EhValido()
        {
            var clienteOriginal = _clienteRepository.ObterPorId(Entidade.Id).Result;

            return clienteOriginal?.CPF == Entidade.CPF;
        }
    }
}
