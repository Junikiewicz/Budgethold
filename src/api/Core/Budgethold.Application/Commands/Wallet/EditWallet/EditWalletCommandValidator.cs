using FluentValidation;
namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandValidator : AbstractValidator<EditWalletCommand>
    {
        public EditWalletCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.StartingValue).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UsersId).NotEmpty();
            RuleForEach(x => x.UsersId).NotEmpty().GreaterThan(0);
        }
    }
}
