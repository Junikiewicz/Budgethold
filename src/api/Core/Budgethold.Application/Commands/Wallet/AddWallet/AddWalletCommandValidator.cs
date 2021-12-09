using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public class AddWalletCommandValidator : AbstractValidator<AddWalletCommand>
    {
        public AddWalletCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.UserIds).NotEmpty();
            RuleForEach(x => x.UserIds).GreaterThan(0);
        }
    }
}
