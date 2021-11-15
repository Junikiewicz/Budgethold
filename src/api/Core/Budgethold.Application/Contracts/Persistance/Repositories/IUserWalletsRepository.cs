using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IUserWalletsRepository : IGenericRepository<UserWallet>
    {
        Task<bool> CheckIfUserIsWalletOwnerAsync(int walletId, int userId, CancellationToken cancellationToken);
    }
}
