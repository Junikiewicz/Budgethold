using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, string propertyName)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                var isActiveProperty = type.FindProperty(propertyName);
                if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(type.ClrType, "p");
                    var filter = Expression.Lambda(Expression.Not(Expression.Property(parameter, isActiveProperty.PropertyInfo!)), parameter);
                    type.SetQueryFilter(filter);
                }
            }
        }
    }
}
