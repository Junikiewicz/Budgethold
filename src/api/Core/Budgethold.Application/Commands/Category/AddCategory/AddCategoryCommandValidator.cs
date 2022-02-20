using Budgethold.Domain.Enums;
using Budgethold.ValidationExtensions;
using FluentValidation;

namespace Budgethold.Application.Commands.Category.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ParentCategoryId).GreaterThan(0).When(x => x.ParentCategoryId.HasValue);
            RuleFor(x => x.TransactionTypeId).GreaterThan(0).CanBeCastedToEnum(typeof(TransactionType));
        }
    }
}
