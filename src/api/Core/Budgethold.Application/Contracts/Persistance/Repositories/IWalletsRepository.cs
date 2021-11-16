using Budgethold.Application.Queries.Wallet.GetSingleWalletQuery;
using Budgethold.Domain.Models;
using WalletResponse = Budgethold.Application.Queries.Wallet.GetUserWallets.WalletResponse;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IWalletsRepository : IGenericRepository<Wallet>
    {
        Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken);
        Task<Wallet?> GetWalletWithUserWalletsAsync(int walletId, CancellationToken cancellationToken);
        Task<SingleWalletResponse?> GetWalletForViewAsync(int walletId, CancellationToken cancellationToken);
    }
}
