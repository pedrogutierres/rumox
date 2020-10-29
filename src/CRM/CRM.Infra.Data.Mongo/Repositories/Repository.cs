using Core.Domain.Interfaces;
using Core.Domain.Models;
using CRM.Infra.Data.Mongo.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Infra.Data.Mongo.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly CRMContext Context;
        protected IMongoCollection<TEntity> Collection;

        protected Repository(CRMContext context)
        {
            Context = context;
            Collection = context.Db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual Task Registrar(TEntity obj)
        {
            return Collection.InsertOneAsync(obj);
        }

        public virtual Task Atualizar(TEntity obj)
        {
            return Collection.FindOneAndReplaceAsync(p => p.Id == obj.Id, obj);
        }

        public virtual Task Deletar(Guid id)
        {
            return Collection.FindOneAndDeleteAsync(p => p.Id == id);
        }

        public virtual IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate);
        }

        public virtual Task<TEntity> ObterPorId(Guid id)
        {
            return Collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public virtual Task<bool> ExistePorId(Guid id)
        {
            return Collection.Find(p => p.Id == id).Limit(1).AnyAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
