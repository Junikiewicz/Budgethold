using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class UserWalletsRepository : GenericRepository<UserWallet>, IUserWalletsRepository
    {
        public UserWalletsRepository(DataContext context) : base(context) { }

        public async Task<bool> CheckIfUserIsAssignedToWalletAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            return await Context.UserWallets.Where(x => x.WalletId == walletId)
                .Select(x => x.UserId).ContainsAsync(userId, cancellationToken);
        }
    }
}
