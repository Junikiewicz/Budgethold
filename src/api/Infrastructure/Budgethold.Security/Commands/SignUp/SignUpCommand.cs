using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Security.Commands.SignUp
{
    public class SignUpCommand : IRequest<Result>
    {
        public SignUpCommand(string email, string password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
