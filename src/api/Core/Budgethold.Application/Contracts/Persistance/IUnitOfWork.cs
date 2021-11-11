using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Domain.Common;

namespace Budgethold.Application.Contracts.Persistance
{
    public interface IUnitOfWork
    {
        IWalletsRepository WalletsRepository { get; }

        public Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
