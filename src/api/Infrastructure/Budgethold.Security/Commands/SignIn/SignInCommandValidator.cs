﻿using Budgethold.Security.Commands.SignUp;
using Budgethold.ValidationExtensions;
using FluentValidation;

namespace Budgethold.Security.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignInCommandValidator()
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
