using Budgethold.Domain.Common;
using Budgethold.Security.Extensions;
using Budgethold.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Commands.SignUp
{
    internal class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly UserManager<User> _userManager;

        public SignUpCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Email);
            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded) return identityResult.ToResult();

            return new Result();
        }
    }
}
