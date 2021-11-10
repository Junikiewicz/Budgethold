using Budgethold.Domain.Common.Errors;

namespace Budgethold.Security.Common.Errors
{
    public class AuthError : Error
    {
        public AuthError(string message) : base(message) { }
    }
}
