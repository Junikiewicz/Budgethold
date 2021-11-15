using FluentValidation;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletQuery
{
    public class GetUserWalletQueryValidator : AbstractValidator<GetUserWalletQuery>
    {
        public GetUserWalletQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.WalletId).GreaterThan(0);
        }
    {
    }
}
