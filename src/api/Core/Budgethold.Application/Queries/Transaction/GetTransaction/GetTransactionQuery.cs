using Budgethold.Application.Queries.Transaction.Common;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetTransaction
{
    public record GetTransactionQuery : IRequest<Result<TransactionResponse>>
    {
        public GetTransactionQuery(int userId, int transactionId)
        {
            UserId = userId;
            TransactionId = transactionId;
        }

        public int UserId { get; init; }
        public int TransactionId { get; init; }
    }
}
