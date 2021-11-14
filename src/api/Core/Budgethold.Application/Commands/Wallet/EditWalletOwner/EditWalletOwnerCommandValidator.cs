using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Commands.Wallet.EditWalletOwner
{
    public class EditWalletOwnerCommandValidator : AbstractValidator<EditWalletOwnerCommand>
    {
        public EditWalletOwnerCommandValidator()
        {
            RuleFor(x => x.WalletId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.NewOwnerId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
