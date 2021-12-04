using FluentValidation;

namespace Budgethold.Application.Commands.Transaction.EditTransaction
{
    public class EditTransactionCommandValidator : AbstractValidator<EditTransactionCommand>
    {
        public EditTransactionCommandValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
