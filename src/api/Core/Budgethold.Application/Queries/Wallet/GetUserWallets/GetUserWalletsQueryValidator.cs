using FluentValidation;

namespace Budgethold.Application.Queries.Wallet.GetUserWallets
{
    public class GetUserWalletsQueryValidator : AbstractValidator<GetUserWalletsQuery>
    {
        public GetUserWalletsQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}