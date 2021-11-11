using Budgethold.Domain.Common;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);
    }
}
