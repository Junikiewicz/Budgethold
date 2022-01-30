using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Common.Extensions;
using Budgethold.Domain.Common;

namespace Budgethold.Persistance.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        public GenericRepository(DataContext context)
        {
            Context = context;
        }

        protected DataContext Context { get; init; }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            entity.SetIsDeleted();
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entites)
        {
            entites.ForEach(x => Remove(x));
        }

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }
}
