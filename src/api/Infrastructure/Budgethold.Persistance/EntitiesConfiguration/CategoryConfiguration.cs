using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgethold.Persistance.EntitiesConfiguration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired(true);

            builder.HasOne(x => x.TransactionType)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.TransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ParentCategory)
                .WithMany(x => x.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Wallet)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.WalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}