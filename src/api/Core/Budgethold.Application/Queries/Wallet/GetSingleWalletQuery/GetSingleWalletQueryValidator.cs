using FluentValidation;

namespace Budgethold.Application.Queries.Wallet.GetSingleWalletQuery
{
    public class GetSingleWalletQueryValidator : AbstractValidator<GetSingleWalletQuery>
    {
        public GetSingleWalletQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.WalletId).GreaterThan(0);
        }
    }
}
