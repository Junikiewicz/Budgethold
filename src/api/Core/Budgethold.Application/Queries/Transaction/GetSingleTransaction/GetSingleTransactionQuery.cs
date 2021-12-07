using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetSingleTransaction
{
    public record GetSingleTransactionQuery : IRequest<Result<TransactionResponse>>
    {
        public GetSingleTransactionQuery(int userId, int transactionId)
        {
            UserId = userId;
            TransactionId = transactionId;
        }

        public int UserId { get; init; }
        public int TransactionId { get; init; }
    }
}
