using Budgethold.Domain.Common.Errors;

namespace Budgethold.Security.Common.Errors
{
    public record AuthError : Error
    {
        public AuthError(string message) : base(message) { }
    }
}
