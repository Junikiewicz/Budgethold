using Budgethold.Domain.Common;
using MediatR;
using System.Runtime.InteropServices;

namespace Budgethold.Application.Commands.Transaction.EditTransaction
{
    public record EditTransactionCommand : IRequest<Result>
    {
        public EditTransactionCommand(int transactionId, int userId, string name, string description, int categoryId, decimal amount, DateTime date, int walletId)
        {
            TransactionId = transactionId;
            UserId = userId;
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Amount = amount;
            Date = date;
            WalletId = walletId;
        }

        public int WalletId { get; init; }
        public int TransactionId { get; init; }
        public int UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int CategoryId { get; init; }
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
    }
}
