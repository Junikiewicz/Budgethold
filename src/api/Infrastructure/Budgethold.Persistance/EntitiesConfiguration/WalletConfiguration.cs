using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgethold.Persistance.EntitiesConfiguration
{
    internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.CurrentValue)
                .HasPrecision(19, 4);
            builder.Property(x => x.StartingValue)
                .HasPrecision(19, 4);
            builder.HasOne(w => w.OwningUser)
                .WithMany(u => u.OwnedWallets)
                .HasForeignKey(uwu => uwu.OwningUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
