using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetCategory
{
    public record GetCategoryQuery : IRequest<Result<CategoryResponse>>
    {
        public GetCategoryQuery(int categoryId, int userId)
        {
            CategoryId = categoryId;
            UserId = userId;
        }

        public int CategoryId { get; init; }
        public int UserId { get; init; }
    }
}
