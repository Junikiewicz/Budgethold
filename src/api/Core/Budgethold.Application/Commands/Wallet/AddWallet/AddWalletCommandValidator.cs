using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    internal class AddWalletCommandValidator : AbstractValidator<AddWalletCommand>
    {
        public AddWalletCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
