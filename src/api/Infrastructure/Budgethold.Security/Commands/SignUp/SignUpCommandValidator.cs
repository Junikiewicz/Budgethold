using Budgethold.ValidationExtensions;
using FluentValidation;

namespace Budgethold.Security.Commands.SignUp
{
    internal class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Email)
             .NotEmpty()
             .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .OneOrMoreCapitalLetters()
                .OneOrMoreLowercaseLetters()
                .OneOrMoreDigit()
                .OneOrMoreSpecialCharaceters();
        }
    }
}
