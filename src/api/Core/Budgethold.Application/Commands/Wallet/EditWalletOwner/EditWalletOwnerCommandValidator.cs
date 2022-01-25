﻿using FluentValidation;

namespace Budgethold.Application.Commands.Wallet.EditWalletOwner
{
    public class EditWalletOwnerCommandValidator : AbstractValidator<EditWalletOwnerCommand>
    {
        public EditWalletOwnerCommandValidator()
        {
            RuleFor(x => x.WalletId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.NewOwnerId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
