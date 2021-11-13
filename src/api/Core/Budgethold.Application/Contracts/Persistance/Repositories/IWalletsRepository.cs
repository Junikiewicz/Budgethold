using Budgethold.Application.Queries.Wallet.GetUserWallets;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IWalletsRepository : IGenericRepository<Wallet>
    {
        Task<bool> CheckIfUserIsAssignedToWalletAsync(int walletId, int userId, CancellationToken cancellationToken);
        Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken);
    }
}
