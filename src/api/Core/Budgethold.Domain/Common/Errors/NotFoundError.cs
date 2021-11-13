namespace Budgethold.Domain.Common.Errors
{
    public record NotFoundError : Error
    {
        public NotFoundError(string message) : base(message) { }
    }
}
