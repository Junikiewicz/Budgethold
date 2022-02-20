using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetUserCategories
{
    public record GetUserCategoriesQuery : IRequest<Result<IEnumerable<CategoryTreeResponse>>>
    {
        public GetUserCategoriesQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; init; }
    }
}
