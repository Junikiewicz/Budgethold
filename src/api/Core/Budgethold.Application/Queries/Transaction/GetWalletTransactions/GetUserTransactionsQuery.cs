using Budgethold.Application.Queries.Transaction.Common;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    public record GetUserTransactionsQuery : IRequest<Result<IEnumerable<TransactionResponse>>>
    {
        public GetUserTransactionsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; init; }
    }
}
