using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Queries.Wallet.GetSingleWalletQuery;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;
using WalletResponse = Budgethold.Application.Queries.Wallet.GetUserWallets.WalletResponse;

namespace Budgethold.Persistance.Repositories
{
    internal class WalletsRepository : GenericRepository<Wallet>, IWalletsRepository
    {
        public WalletsRepository(DataContext context) : base(context) { }

        public async Task<Wallet?> GetUserWalletAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Id == walletId).Include(i => i.UserWallets)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.UserWallets.Any(y => y.UserId == userId))
                .Select(x => new WalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                    OwningUsers = x.UserWallets.Select(y => new WalletResponse.OwningUser
                    {
                        Id = y.UserId,
                        Name = y.User.Name
                    })
                }).ToListAsync(cancellationToken);
        }

        public async Task<bool> CheckIfUserIsAssignedToWalletAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Id == walletId)
                .SelectMany(x => x.UserWallets.Select(y => y.UserId))
                .ContainsAsync(userId, cancellationToken);
        }

        public async Task<SingleWalletResponse?> GetUserWalletForViewAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Id == walletId)
                .Select(x => new SingleWalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                    OwningUsers = x.UserWallets.Select(y => new SingleWalletResponse.OwningUser
                    {
                        Id = y.UserId,
                        Name = y.User.Name
                    })
                }).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
