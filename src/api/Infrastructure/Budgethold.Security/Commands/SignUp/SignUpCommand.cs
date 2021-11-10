using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Security.Commands.SignUp
{
    public class SignUpCommand : IRequest<Result>
    {
        public SignUpCommand(string emial, string password)
        {
            Email = emial;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
