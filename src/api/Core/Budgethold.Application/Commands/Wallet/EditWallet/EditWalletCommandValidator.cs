using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandValidator : AbstractValidator<EditWalletCommand>
    {
        public EditWalletCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.StartingValue).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OwnerId).GreaterThan(0);
        }
    }
}
