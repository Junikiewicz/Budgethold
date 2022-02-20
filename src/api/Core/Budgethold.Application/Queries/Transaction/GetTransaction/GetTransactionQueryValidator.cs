using FluentValidation;

namespace Budgethold.Application.Queries.Transaction.GetTransaction
{
    public class GetTransactionQueryValidator : AbstractValidator<GetTransactionQuery>
    {
        public GetTransactionQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.TransactionId).GreaterThan(0);
        }
    }
}
