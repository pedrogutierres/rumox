using Core.Domain.Validations;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.Specifications;
using FluentValidation;

namespace CRM.Domain.Clientes.Validations
{
    public class ClienteAptoParaRegistrarValidation : DomainValidator<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAptoParaRegistrarValidation(Cliente entidade, IClienteRepository clienteRepository) : base(entidade)
        {
            _clienteRepository = clienteRepository;

            ValidarCpfUnico();
            ValidarEmailUnico();
        }

        private void ValidarCpfUnico()
        {
            RuleFor(p => p.CPF)
                .IsValid(new ClienteDeveTerCpfUnicoSpecification(Entidade, _clienteRepository))
                .WithMessage("O CPF do cliente já está sendo utilizado.");
        }
        private void ValidarEmailUnico()
        {
            RuleFor(p => p.Email)
                .IsValid(new ClienteDeveTerCpfEmailSpecification(Entidade, _clienteRepository))
                .WithMessage("O e-mail do cliente já está sendo utilizado.");
        }
    }
}
