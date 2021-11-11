using Budgethold.Domain.Common;
using Budgethold.Security.Common.Errors;
using Budgethold.Security.Constants;
using Budgethold.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Budgethold.Security.Commands.SignIn
{
    internal class SignInCommandHandler : IRequestHandler<SignInCommand, Result>
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;

        public SignInCommandHandler(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user is null) return new Result(new AuthError("Unauthorized"));

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResult.Succeeded) return new Result(new AuthError("Unauthorized"));

            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            (claimsPrincipal.Identity as ClaimsIdentity)!.AddClaims(new[] { new Claim(CustomClaimTypes.UserId, user.Id.ToString()) });

            await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true });

            return new Result();
        }
    }
}
