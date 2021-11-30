using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    public record GetWalletTransactionsQuery : IRequest<Result<TransactionsListResponse>>
    {
        public GetWalletTransactionsQuery(int walletId, int userId)
        {
            WalletId = walletId;
            UserId = userId;
        }

        public int WalletId { get; init; }
        public int UserId { get; init; }
    }
}
