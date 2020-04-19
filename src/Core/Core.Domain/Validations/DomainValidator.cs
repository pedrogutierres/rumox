using Core.Domain.Models;
using FluentValidation;

namespace Core.Domain.Validations
{
    public abstract class DomainValidator<TEntity> : AbstractValidator<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly TEntity _entidade;

        protected DomainValidator(TEntity entidade)
        {
            _entidade = entidade;
        }
    }
}
