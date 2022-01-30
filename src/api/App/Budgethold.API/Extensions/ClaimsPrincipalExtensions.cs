using System.Security.Claims;
using Budgethold.Security.Constants;

namespace Budgethold.API.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.FindFirstValue(CustomClaimTypes.UserId));
        }
    }
}
