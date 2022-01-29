using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Security.Commands.SignIn
{
    public class SignInCommand : IRequest<Result>
    {
        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; init; }

        public string Password { get; init; }
    }
}
