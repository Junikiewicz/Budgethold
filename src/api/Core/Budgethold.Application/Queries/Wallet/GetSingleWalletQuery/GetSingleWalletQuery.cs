using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetSingleWalletQuery
{
    public record GetSingleWalletQuery : IRequest<Result<SingleWalletResponse>>
    {
        public GetSingleWalletQuery(int walletId, int userId)
        {
            WalletId = walletId;
            UserId = userId;
        }

        public int WalletId { get; init; }
        public int UserId { get; init; }
    }
}
