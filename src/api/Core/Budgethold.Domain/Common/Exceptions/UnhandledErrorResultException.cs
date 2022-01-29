namespace Budgethold.Domain.Common.Exceptions
{
    internal class UnhandledErrorResultException : Exception
    {
        public UnhandledErrorResultException(string message) : base(message) { }
    }
}
