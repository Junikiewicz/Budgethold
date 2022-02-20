using FluentValidation;

namespace Budgethold.Application.Queries.Wallet.GetWalletQuery
{
    public class GetWalletQueryValidator : AbstractValidator<GetWalletQuery>
    {
        public GetWalletQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.WalletId).GreaterThan(0);
        }
    }
}
