using Budgethold.Domain.Common;
using Budgethold.Security.Common.Errors;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Extensions
{
    internal static class IdentityResultExtensions
    {
        public static Result ToResult(this IdentityResult identityResult)
        {
            return new Result(new AuthError(string.Join("\n", identityResult.Errors.Select(x => x.Description))));
        }
    }
}
