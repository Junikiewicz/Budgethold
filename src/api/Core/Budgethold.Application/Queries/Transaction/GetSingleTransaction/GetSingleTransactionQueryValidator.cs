using FluentValidation;

namespace Budgethold.Application.Queries.Transaction.GetSingleTransaction
{
    public class GetSingleTransactionQueryValidator : AbstractValidator<GetSingleTransactionQuery>
    {
        public GetSingleTransactionQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.TransactionId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
