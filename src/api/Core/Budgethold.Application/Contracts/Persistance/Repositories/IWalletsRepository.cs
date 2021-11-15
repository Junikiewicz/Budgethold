using Budgethold.Application.Queries.Wallet.GetSingleWalletQuery;
using Budgethold.Domain.Models;
using WalletResponse = Budgethold.Application.Queries.Wallet.GetUserWallets.WalletResponse;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IWalletsRepository : IGenericRepository<Wallet>
    {
        Task<bool> CheckIfUserIsAssignedToWalletAsync(int walletId, int userId, CancellationToken cancellationToken);
        Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken);
        Task<Wallet?> GetUserWalletAsync(int walletId, int userId, CancellationToken cancellationToken);
        Task<SingleWalletResponse?> GetUserWalletForViewAsync(int walletId, int userId, CancellationToken cancellationToken);
    }
}
