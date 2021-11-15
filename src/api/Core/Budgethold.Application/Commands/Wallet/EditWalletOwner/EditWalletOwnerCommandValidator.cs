using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.EditWalletOwner
{
    public class EditWalletOwnerCommandValidator : AbstractValidator<EditWalletOwnerCommand>
    {
        public EditWalletOwnerCommandValidator()
        {
            RuleFor(x => x.WalletId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.NewOwnerId).NotEmpty().GreaterThan(0);
        }
    }
}
