using Budgethold.Domain.Models;
using WalletResponse = Budgethold.Application.Queries.Wallet.Common.WalletResponse;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IWalletsRepository : IGenericRepository<Wallet>
    {
        Task<Wallet> GetWalletAsync(int walletId, CancellationToken cancellationToken);

        Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken);

        Task<WalletResponse> GetWalletResponseAsync(int walletId, CancellationToken cancellationToken);

        Task<Wallet?> GetWalletOrDefaultAsync(int walletId, CancellationToken cancellationToken);

        Task<bool> IsWalletAssignedToUserAsync(int walletId, int userId, CancellationToken cancellationToken);
    }
}
