using Budgethold.Domain.Models;
using Budgethold.Security.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgethold.Persistance.EntitiesConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable($"{nameof(AspNetUser)}s");

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName(nameof(User.IsDeleted));

            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired(true);
        }
    }
}
