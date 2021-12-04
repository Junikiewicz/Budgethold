using Budgethold.Application.Queries.Transaction.GetSingleTransaction;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    public record GetWalletTransactionsQuery : IRequest<Result<IEnumerable<TransactionResponse>>>
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
