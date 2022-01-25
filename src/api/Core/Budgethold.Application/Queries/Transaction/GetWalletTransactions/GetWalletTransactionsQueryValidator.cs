using FluentValidation;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    public class GetWalletTransactionsQueryValidator : AbstractValidator<GetWalletTransactionsQuery>
    {
        public GetWalletTransactionsQueryValidator()
        {
            RuleFor(x => x.WalletId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
