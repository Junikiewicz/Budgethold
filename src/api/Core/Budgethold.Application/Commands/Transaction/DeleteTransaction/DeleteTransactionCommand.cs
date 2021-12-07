using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Transaction.DeleteTransaction
{
    public record DeleteTransactionCommand : IRequest<Result>
    {
        public DeleteTransactionCommand(int userId, int transactionId)
        {
            UserId = userId;
            TransactionId = transactionId;
        }

        public int UserId { get; init; }
        public int TransactionId { get; init; }
    }
}
