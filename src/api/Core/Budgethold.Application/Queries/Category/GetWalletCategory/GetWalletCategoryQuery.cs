using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetWalletCategory
{
    public record GetWalletCategoryQuery : IRequest<Result<CategoryResponse>>
    {
        public GetWalletCategoryQuery(int categoryId, int userId)
        {
            CategoryId = categoryId;
            UserId = userId;
        }

        public int CategoryId { get; init; }
        public int UserId { get; init; }
    }
}
