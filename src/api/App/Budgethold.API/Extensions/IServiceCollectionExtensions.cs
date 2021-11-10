using Budgethold.API.Common.Validation;
using Budgethold.Security.Commands.SignIn;
using FluentValidation;
using MediatR;

namespace Budgethold.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(SignInCommandValidator).Assembly).ForEach(item => serviceCollection.AddScoped(item.InterfaceType, item.ValidatorType));

            return serviceCollection;
        }
    }
}
