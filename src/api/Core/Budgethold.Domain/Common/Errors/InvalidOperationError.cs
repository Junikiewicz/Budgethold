namespace Budgethold.Domain.Common.Errors
{
    public record InvalidOperationError : Error
    {
        public InvalidOperationError(string message) : base(message) { }
    }
}
