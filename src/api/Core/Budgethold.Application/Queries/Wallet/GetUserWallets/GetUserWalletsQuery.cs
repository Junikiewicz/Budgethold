using Budgethold.Application.Queries.Wallet.Common;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetUserWallets
{
    public record GetUserWalletsQuery : IRequest<Result<IEnumerable<WalletResponse>>>
    {
        public GetUserWalletsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; init; }
    }
}
