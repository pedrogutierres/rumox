using Core.Domain.Interfaces;
using Core.Domain.Models;
using CRM.Infra.Data.Mongo.Context;
using MongoDB.Driver;
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

        public virtual async Task Registrar(TEntity obj)
        {
            await Collection.InsertOneAsync(obj);
        }

        public virtual async Task Atualizar(TEntity obj)
        {
            await Collection.FindOneAndReplaceAsync(p => p.Id == obj.Id, obj);
        }

        public virtual async Task Deletar(Guid id)
        {
            await Collection.FindOneAndDeleteAsync(p => p.Id == id);
        }

        public virtual IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate);
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return (await Collection.FindAsync(p => p.Id == id)).FirstOrDefault();
        }

        public virtual async Task<bool> ExistePorId(Guid id)
        {
            return await Collection.CountDocumentsAsync(p => p.Id == id) > 0;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
