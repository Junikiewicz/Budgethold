using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public class AddWalletCommandValidator : AbstractValidator<AddWalletCommand>
    {
        public AddWalletCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Ids).NotEmpty();
            RuleForEach(x => x.Ids).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
