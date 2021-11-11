using Budgethold.Domain.Common;
using Budgethold.Security.Extensions;
using Budgethold.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Commands.SignUp
{
    internal class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly UserManager<AspNetUser> _userManager;

        public SignUpCommandHandler(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var user = new AspNetUser(command.Email, command.Name);
            var identityResult = await _userManager.CreateAsync(user, command.Password);

            if (!identityResult.Succeeded) return identityResult.ToResult();

            return new Result();
        }
    }
}
