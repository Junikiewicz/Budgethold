using FluentValidation;

namespace Budgethold.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}