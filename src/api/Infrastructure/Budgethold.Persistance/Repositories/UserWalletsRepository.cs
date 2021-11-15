using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class UserWalletsRepository : GenericRepository<UserWallet>, IUserWalletsRepository
    {
        public UserWalletsRepository(DataContext context) : base(context) { }


        public async Task<bool> CheckIfUserIsWalletOwnerAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            var userWallet = await _context.UserWallets.SingleOrDefaultAsync(x => x.WalletId == walletId && x.UserId == userId, cancellationToken);
            return userWallet is null ? false : userWallet.IsOwner;
        }
    }
}
