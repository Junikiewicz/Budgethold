using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandValidator : AbstractValidator<EditWalletCommand>
    {
        public EditWalletCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.UserIds).NotEmpty();
            RuleForEach(x => x.UserIds).GreaterThan(0);
        }
    }
}
