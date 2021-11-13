namespace Budgethold.Domain.Common.Errors
{
    public abstract record Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; init; }
    }
}
