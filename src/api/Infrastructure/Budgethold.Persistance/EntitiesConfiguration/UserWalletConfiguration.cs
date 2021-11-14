using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Persistance.EntitiesConfiguration
{
    internal class UserWalletConfiguration : IEntityTypeConfiguration<UserWallet>
    {
        public void Configure(EntityTypeBuilder<UserWallet> builder)
        {
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasKey(uw => new { uw.WalletId, uw.UserId });

            builder.HasOne(uw => uw.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(uw => uw.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uw => uw.Wallet)
                .WithMany(w => w.Users)
                .HasForeignKey(uw => uw.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
