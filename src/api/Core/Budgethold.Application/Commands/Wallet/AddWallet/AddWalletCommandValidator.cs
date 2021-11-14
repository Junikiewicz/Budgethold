using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public class AddWalletCommandValidator : AbstractValidator<AddWalletCommand>
    {
        public AddWalletCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Users).NotEmpty();
            RuleForEach(x => x.Users).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
