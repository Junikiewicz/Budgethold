using Budgethold.Application.Queries.Wallet.GetUserWalletsQuery;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface IWalletsRepository : IGenericRepository<Wallet>
    {
        Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken);
        Task<WalletResponse> GetUserWalletAsync(int walletId, int userId, CancellationToken cancellationToken);
    }
}
