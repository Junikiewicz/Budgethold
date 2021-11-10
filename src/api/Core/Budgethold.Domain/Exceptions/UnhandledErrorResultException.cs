namespace Budgethold.Domain.Exceptions
{
    internal class UnhandledErrorResultException : Exception
    {
        public UnhandledErrorResultException(string message) : base(message)
        {

        }
    }
}
