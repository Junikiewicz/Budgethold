using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public DeleteCategoryCommand(int categoryId, int userId)
        {
            CategoryId = categoryId;
            UserId = userId;
        }

        public int CategoryId { get; init; }
        public int UserId { get; init; }
    }
}
