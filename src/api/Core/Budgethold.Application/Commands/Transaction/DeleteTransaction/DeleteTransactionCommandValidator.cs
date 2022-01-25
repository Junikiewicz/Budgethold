using FluentValidation;

namespace Budgethold.Application.Commands.Transaction.DeleteTransaction
{
    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
    {
        public DeleteTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
