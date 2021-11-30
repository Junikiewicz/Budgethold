using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Transaction.EditTransaction
{
    public record EditTransactionCommand : IRequest<Result>
    {
        public EditTransactionCommand(int transactionId, int userId, string name, string description, int walletId, int categoryId, decimal amount, DateTime date)
        {
            TransactionId = transactionId;
            UserId = userId;
            Name = name;
            Description = description;
            WalletId = walletId;
            CategoryId = categoryId;
            Amount = amount;
            Date = date;
        }

        public int TransactionId { get; init; }
        public int UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int WalletId { get; init; }
        public int CategoryId { get; init; }
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
    }
}
