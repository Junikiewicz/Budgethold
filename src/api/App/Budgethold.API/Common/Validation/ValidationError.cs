using Budgethold.Domain.Common.Errors;
using FluentValidation.Results;
using System.Text;

namespace Budgethold.API.Common.Validation
{
    internal record ValidationError : Error
    {
        public ValidationError(IEnumerable<ValidationFailure> failures) : base(GetErrorMessage(failures)) { }

        private static string GetErrorMessage(IEnumerable<ValidationFailure> failures)
        {
            var errorBuilder = new StringBuilder();

            errorBuilder.AppendLine("Invalid command, reason: ");

            foreach (var error in failures)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            return errorBuilder.ToString();
        }
    }
}
