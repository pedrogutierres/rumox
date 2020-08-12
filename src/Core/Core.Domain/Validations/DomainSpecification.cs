using Core.Domain.Models;
using System.Threading.Tasks;

namespace Core.Domain.Validations
{
    public abstract class DomainSpecification<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly TEntity Entidade;

        protected DomainSpecification(TEntity entidade)
        {
            Entidade = entidade;
        }

        public abstract Task<bool> EhValido();
    }
}
