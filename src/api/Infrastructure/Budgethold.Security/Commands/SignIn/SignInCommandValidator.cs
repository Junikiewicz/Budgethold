using Budgethold.Security.Commands.SignUp;
using FluentValidation;

namespace Budgethold.Security.Commands.SignIn
{
    internal class SignInCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
