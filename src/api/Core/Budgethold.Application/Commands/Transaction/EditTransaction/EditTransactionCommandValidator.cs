using FluentValidation;

namespace Budgethold.Application.Commands.Transaction.EditTransaction
{
    public class EditTransactionCommandValidator : AbstractValidator<EditTransactionCommand>
    {
        public EditTransactionCommandValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("The field {PropertyName} is required.").MaximumLength(100);
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
