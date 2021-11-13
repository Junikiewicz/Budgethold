using Budgethold.API.Common.Validation;
using Budgethold.Application.Commands.Category.AddCategory;
using Budgethold.Security.Commands.SignIn;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Budgethold.API.Extensions
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            var assemblies = new List<Assembly>() { typeof(SignInCommandValidator).Assembly, typeof(AddCategoryCommandValidator).Assembly };

            AssemblyScanner.FindValidatorsInAssemblies(assemblies).ForEach(item => serviceCollection.AddScoped(item.InterfaceType, item.ValidatorType));

            return serviceCollection;
        }
    }
}
