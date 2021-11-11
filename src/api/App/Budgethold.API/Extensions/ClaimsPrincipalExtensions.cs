using Budgethold.Security.Constants;
using System.Security.Claims;

namespace Budgethold.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.FindFirstValue(CustomClaimTypes.UserId));
        }
    }
}
