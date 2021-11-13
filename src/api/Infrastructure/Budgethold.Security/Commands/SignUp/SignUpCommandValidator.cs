using Budgethold.ValidationExtensions;
using FluentValidation;

namespace Budgethold.Security.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
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
                .OneOrMoreDigits()
                .OneOrMoreSpecialCharacters();
        }
    }
}
