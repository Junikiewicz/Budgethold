using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Common.Extensions;
using Budgethold.Domain.Common;

namespace Budgethold.Persistance.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
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
            _context.Set<TEntity>().Update(entity);
        }
    }
}
