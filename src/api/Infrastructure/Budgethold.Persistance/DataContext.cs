using System.Reflection;
using Budgethold.Domain.Common;
using Budgethold.Domain.Models;
using Budgethold.Persistance.Extensions;
using Budgethold.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance
{
    public class DataContext : IdentityDbContext<AspNetUser, AspNetRole, int, IdentityUserClaim<int>, AspNetUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions) { }

        public DbSet<User> DomainUsers => Set<User>();

        public DbSet<Transaction> Transactions => Set<Transaction>();

        public DbSet<Wallet> Wallets => Set<Wallet>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<UserWallet> UserWallets => Set<UserWallet>();

        public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SetSoftDeleteFilter(nameof(IEntity.IsDeleted));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
