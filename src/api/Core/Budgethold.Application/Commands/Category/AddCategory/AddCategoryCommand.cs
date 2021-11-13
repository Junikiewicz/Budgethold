using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Category.AddCategory
{
    public record AddCategoryCommand : IRequest<Result>
    {
        public AddCategoryCommand(int userId, string name, int? parentCategoryId, int transactionTypeId, int walletId)
        {
            UserId = userId;
            Name = name;
            ParentCategoryId = parentCategoryId;
            TransactionTypeId = transactionTypeId;
            WalletId = walletId;
        }

        public int UserId { get; init; }
        public string Name { get; init; }
        public int? ParentCategoryId { get; init; }
        public int TransactionTypeId { get; init; }
        public int WalletId { get; init; }
    }
}
