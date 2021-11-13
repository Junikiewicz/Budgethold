using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Queries.Wallet.GetUserWalletsQuery;
using Budgethold.Common.Extensions;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class WalletsRepository : GenericRepository<Wallet>, IWalletsRepository
    {
        public WalletsRepository(DataContext context) : base(context)
        {
        }

        public override void Add(Wallet entity)
        {
            _context.Wallets!.Add(entity);

            entity.Users.ForEach(x => _context.Attach(x));
        }

        public async Task<WalletResponse> GetUserWalletAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets!
                .Where(x => x.Users.Any(y => y.Id == userId) && x.Id == walletId)
                .Select(x => new WalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                    OwningUsers = x.Users.Select(y => new WalletResponse.OwningUser
                    {
                        Id = y.Id,
                        Name = y.Name
                    })
                }).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        }

        public async Task<IEnumerable<WalletResponse>> GetUserWalletsAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets!
                .Where(x => x.Users.Any(y => y.Id == userId))
                .Select(x => new WalletResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurrentValue = x.CurrentValue,
                    StartingValue = x.StartingValue,
                    OwningUsers = x.Users.Select(y => new WalletResponse.OwningUser
                    {
                        Id = y.Id,
                        Name = y.Name
                    }) 
                }).ToListAsync(cancellationToken);
        }
    }
}
