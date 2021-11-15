using FluentValidation;
namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandValidator : AbstractValidator<EditWalletCommand>
    {
        public EditWalletCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.StartingValue).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Users).NotEmpty();
            RuleForEach(x => x.Users).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
