using FluentValidation;
namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandValidator : AbstractValidator<EditWalletCommand>
    {
        public EditWalletCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("The field {PropertyName} is required.").MaximumLength(100);
            RuleFor(x => x.UserIds).NotEmpty().WithMessage("The field {PropertyName} is required.");
            RuleForEach(x => x.UserIds).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
