using Core.Domain.Validations;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.Specifications;
using FluentValidation;

namespace CRM.Domain.Clientes.Validations
{
    public class ClienteAptoParaAtualizarValidation : DomainValidator<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAptoParaAtualizarValidation(Cliente entidade, IClienteRepository clienteRepository) : base(entidade)
        {
            _clienteRepository = clienteRepository;

            ValidarCpf();
            ValidarEmailUnico();
        }

        private void ValidarCpf()
        {
            RuleFor(p => p.CPF)
                .IsValid(new ClienteNaoDeveAlterarCpfSpecification(Entidade, _clienteRepository))
                .WithMessage("O CPF do cliente não pode ser alterado.");
        }
        private void ValidarEmailUnico()
        {
            RuleFor(p => p.Email)
                .IsValid(new ClienteDeveTerCpfEmailSpecification(Entidade, _clienteRepository))
                .WithMessage("O e-mail do cliente já está sendo utilizado.");
        }
    }
}
