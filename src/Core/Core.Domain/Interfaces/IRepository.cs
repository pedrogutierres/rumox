using Core.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        Task Registrar(TEntity obj);
        Task Atualizar(TEntity obj);
        Task Deletar(Guid id);

        Task<TEntity> ObterPorId(Guid id);
        Task<bool> ExistePorId(Guid id);
        IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
