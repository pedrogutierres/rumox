using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IReadRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        Task<TEntity> ObterPorId(Guid id);
        Task<bool> ExistePorId(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    }
}
