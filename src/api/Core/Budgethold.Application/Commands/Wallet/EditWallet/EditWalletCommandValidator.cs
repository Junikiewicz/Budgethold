using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandValidator : AbstractValidator<EditWalletCommand>
    {
        public EditWalletCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
