using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetWalletCategories
{
    public record GetWalletCategoriesQuery : IRequest<Result<IEnumerable<CategoryTreeResponse>>>
    {
        public GetWalletCategoriesQuery(int userId, int walletId)
        {
            UserId = userId;
            WalletId = walletId;
        }

        public int UserId { get; init; }
        public int WalletId { get; init; }
    }
}
