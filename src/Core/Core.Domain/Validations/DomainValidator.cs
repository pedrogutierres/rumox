using Core.Domain.Models;
using FluentValidation;

namespace Core.Domain.Validations
{
    public abstract class DomainValidator<TEntity> : AbstractValidator<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly TEntity Entidade;

        protected DomainValidator(TEntity entidade)
        {
            Entidade = entidade;

            ValidarId();
        }

        private void ValidarId()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O id deve ser informado corretamente.");
        }
    }
}
