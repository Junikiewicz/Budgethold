using FluentValidation;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    public class GetUserTransactionsQueryValidator : AbstractValidator<GetUserTransactionsQuery>
    {
        public GetUserTransactionsQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
