using Budgethold.Domain.Models;
using Budgethold.Security.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgethold.Persistance.EntitiesConfiguration
{
    internal class AspNetUserConfiguration : IEntityTypeConfiguration<AspNetUser>
    {
        public void Configure(EntityTypeBuilder<AspNetUser> builder)
        {
            builder.ToTable($"{nameof(AspNetUser)}s");

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName(nameof(AspNetUser.IsDeleted));
            builder.Property(e => e.UserName)
                .HasColumnName(nameof(AspNetUser.UserName));

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<User>(e => e.Id)
                .IsRequired();

            builder.Navigation(x => x.User)
                .IsRequired();
        }
    }
}
