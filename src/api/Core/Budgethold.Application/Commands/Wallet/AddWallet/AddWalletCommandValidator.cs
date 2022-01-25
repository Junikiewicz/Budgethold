using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public class AddWalletCommandValidator : AbstractValidator<AddWalletCommand>
    {
        public AddWalletCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The field {PropertyName} is required.").MaximumLength(100);
            RuleForEach(x => x.UserIds).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
