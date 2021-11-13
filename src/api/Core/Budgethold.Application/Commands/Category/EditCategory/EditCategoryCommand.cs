using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Category.EditCategory
{
    public record EditCategoryCommand : IRequest<Result>
    {
        public EditCategoryCommand(int categoryId, int userId, string name, int? parentCategoryId)
        {
            CategoryId = categoryId;
            UserId = userId;
            Name = name;
            ParentCategoryId = parentCategoryId;
        }

        public int CategoryId { get; init; }
        public int UserId { get; init; }
        public string Name { get; init; }
        public int? ParentCategoryId { get; init; }
    }
}
