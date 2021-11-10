using Budgethold.Domain.Common;

namespace Budgethold.Application.Contracts.Persistance
{
    public interface IUnitOfWork
    {
        public Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
