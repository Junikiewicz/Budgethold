using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgethold.Persistance.EntitiesConfiguration
{
    internal class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired(true);

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<TransactionType> builder)
        {
            var enumValues = (Domain.Enums.TransactionType[])Enum.GetValues(typeof(Domain.Enums.TransactionType));

            builder.HasData(enumValues.Select(x => new TransactionType(x)));
        }
    }
}
