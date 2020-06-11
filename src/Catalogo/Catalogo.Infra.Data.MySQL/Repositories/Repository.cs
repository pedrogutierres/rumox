using Catalogo.Infra.Data.MySQL.Context;
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalogo.Infra.Data.MySQL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected CatalogoContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(CatalogoContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task Registrar(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public virtual Task Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
            return Task.CompletedTask;
        }

        public virtual Task Deletar(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            return Task.CompletedTask;
        }

        public virtual IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual Task<TEntity> ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<bool> ExistePorId(Guid id)
        {
            return DbSet.AsNoTracking().AnyAsync(t => t.Id == id);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}