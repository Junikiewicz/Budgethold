using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Queries.Wallet.Common;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class WalletsRepository : GenericRepository<Wallet>, IWalletsRepository
    {
        public WalletsRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken)
        {
            return await Context.Wallets
                .Where(x => x.UserId == userId)
                .Select(x => new WalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                }).ToListAsync(cancellationToken);
        }

        public async Task<WalletResponse> GetWalletResponseAsync(int walletId, CancellationToken cancellationToken)
        {
            return await Context.Wallets
                .Where(x => x.Id == walletId)
                .Select(x => new WalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue
                }).SingleAsync(cancellationToken);
        }

        public async Task<Wallet> GetWalletAsync(int walletId, CancellationToken cancellationToken)
        {
            return await Context.Wallets
               .Where(x => x.Id == walletId)
               .SingleAsync(cancellationToken);
        }

        public async Task<Wallet?> GetWalletOrDefaultAsync(int walletId, CancellationToken cancellationToken)
        {
            return await Context.Wallets
               .Where(x => x.Id == walletId)
               .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsWalletAssignedToUserAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await Context.Wallets.AnyAsync(x => x.Id == walletId && x.UserId == userId, cancellationToken);
        }
    }
}
