using Budgethold.Domain.Common;
using Budgethold.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Commands.SignOut
{
    internal class SignOutCommandHandler : IRequestHandler<SignOutCommand, Result>
    {
        private readonly SignInManager<AspNetUser> _signInManager;

        public SignOutCommandHandler(SignInManager<AspNetUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.Context.SignOutAsync(IdentityConstants.ApplicationScheme);

            return new Result();
        }
    }
}
