using Budgethold.API.Common.Validation;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using Budgethold.Security.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Budgethold.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult GetResponseFromResult(this ControllerBase controllerBase, Result result, string? methodName = null)
        {
            if (result.Succeeded)
            {
                var type = result.GetType();

                var property = type.GetProperty(nameof(Result<object>.Value));

                var createdProperty = type.GetProperty(nameof(CreatedResult<object>.Value));

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(CreatedResult<>) && createdProperty is not null && methodName is not null)
                {
                    var routeValues = new { id = createdProperty.GetValue(result) };
                    var createdResource = new { Id = routeValues.id };

                    return controllerBase.CreatedAtAction(methodName, routeValues, createdResource);
                }

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>) && property is not null)
                {
                    return controllerBase.Ok(property.GetValue(result));
                }

                return controllerBase.NoContent();
            }

            return result.Error switch
            {
                NotFoundError e => controllerBase.BadRequest(e.Message),
                AuthError e => controllerBase.BadRequest(e.Message),
                ValidationError e => controllerBase.BadRequest(e.Message),
                InvalidOperationError e => controllerBase.BadRequest(e.Message),
                _ => throw new NotSupportedException($"Not supported error type: {result.Error}")
            };
        }
    }
}
