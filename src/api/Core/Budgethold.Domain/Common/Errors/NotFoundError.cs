namespace Budgethold.Domain.Common.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message) { }
    }
}
