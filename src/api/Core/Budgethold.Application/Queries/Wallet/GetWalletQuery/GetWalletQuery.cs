using Budgethold.Application.Queries.Wallet.Common;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetWalletQuery
{
    public record GetWalletQuery : IRequest<Result<WalletResponse>>
    {
        public GetWalletQuery(int walletId, int userId)
        {
            WalletId = walletId;
            UserId = userId;
        }

        public int WalletId { get; init; }
        public int UserId { get; init; }
    }
}
