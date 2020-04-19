using Core.Domain.Models;

namespace Core.Domain.Validations
{
    public abstract class DomainSpecification<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly TEntity _entidade;

        protected DomainSpecification(TEntity entidade)
        {
            _entidade = entidade;
        }

        public abstract bool IsValid();
    }
}
