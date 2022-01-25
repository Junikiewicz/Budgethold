using FluentValidation;

namespace Budgethold.Application.Queries.Wallet.GetUserWallets
{
    public class GetUserWalletsQueryValidator : AbstractValidator<GetUserWalletsQuery>
    {
        public GetUserWalletsQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}