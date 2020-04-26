using Core.Domain.Validations;
using FluentValidation;

namespace CRM.Domain.Clientes.Validations
{
    public class ClienteEstaConsistenteValidation : DomainValidator<Cliente>
    {
        public ClienteEstaConsistenteValidation(Cliente entidade) : base(entidade)
        {
            ValidarCPF();
            ValidarNome();
            ValidarSobrenome();
            ValidarEmail();
            ValidarSenha();
        }

        private void ValidarCPF()
        {
            RuleFor(e => e.CPF)
                .NotNull().WithMessage("O CPF do cliente deve ser informado.");

            When(p => p.CPF != null, () =>
            {
                RuleFor(p => p.CPF)
                    .Must(p => p.EhValido()).WithMessage("O CPF está inválido.");
            });
        }
        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do cliente deve ser informado.")
                .MaximumLength(50).WithMessage("O nome do cliente deve conter no máximo {MaxLength} caracateres.");
        }
        private void ValidarSobrenome()
        {
            RuleFor(p => p.Sobrenome)
                .NotEmpty().WithMessage("O sobrenome do cliente deve ser informado.")
                .MaximumLength(100).WithMessage("O sobrenome do cliente deve conter no máximo {MaxLength} caracteres.");
        }
        private void ValidarEmail()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("O e-mail do cliente deve ser informado.")
                .EmailAddress().WithMessage("O e-mail do cliente está inválido.");
        }
        private void ValidarSenha()
        {
            RuleFor(p => p.Senha)
                .NotNull().WithMessage("A senha do cliente deve ser informada.");

            When(p => p.Senha != null, () =>
            {
                RuleFor(p => p.Senha)
                    .Must(p => p.EhValido()).WithMessage("A senha do cliente é inválida.");
            });
        }
    }
}
