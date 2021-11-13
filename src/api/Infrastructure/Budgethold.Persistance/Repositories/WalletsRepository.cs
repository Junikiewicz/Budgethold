using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Queries.Wallet.GetUserWalletQuery;
using Budgethold.Application.Queries.Wallet.GetUserWallets;
using Budgethold.Common.Extensions;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;
using WalletResponse = Budgethold.Application.Queries.Wallet.GetUserWallets.WalletResponse;

namespace Budgethold.Persistance.Repositories
{
    internal class WalletsRepository : GenericRepository<Wallet>, IWalletsRepository
    {
        public WalletsRepository(DataContext context) : base(context) { }

        public override void Add(Wallet entity)
        {
            _context.Wallets.Add(entity);

            entity.Users.ForEach(x => _context.Attach(x));
        }

        public override void Update(Wallet entity)
        {
            _context.Wallets.Update(entity);

            entity.Users.ForEach(x => _context.Attach(x));
        }

        public async Task<Wallet?> GetUserWalletAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Id == walletId && x.OwningUserId == userId)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Users.Any(y => y.Id == userId))
                .Select(x => new WalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerId = x.OwningUserId,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                    OwningUsers = x.Users.Select(y => new WalletResponse.OwningUser
                    {
                        Id = y.Id,
                        Name = y.Name
                    }) 
                }).ToListAsync(cancellationToken);
        }

        public async Task<bool> CheckIfUserIsAssignedToWalletAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Id == walletId)
                .SelectMany(x => x.Users.Select(y => y.Id))
                .ContainsAsync(userId, cancellationToken);
        }

        public async Task<SingleWalletResponse?> GetUserWalletForViewAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Where(x => x.Id == walletId && x.OwningUserId == userId)
                .Select(x => new SingleWalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                    OwnerId = x.OwningUserId,
                    OwningUsers = x.Users.Select(y => new SingleWalletResponse.OwningUser
                    {
                        Id = y.Id,
                        Name = y.Name
                    })
                }).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
