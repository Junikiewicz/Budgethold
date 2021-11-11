using Budgethold.Domain.Common;
using Budgethold.Domain.Models;
using Budgethold.Persistance.Extensions;
using Budgethold.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Budgethold.Persistance
{
    public class DataContext : IdentityDbContext<AspNetUser, AspNetRole, int, IdentityUserClaim<int>, AspNetUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<User>? AppUsers { get; set; }
        public DbSet<Wallet>? Wallets { get; set; }

        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SetSoftDeleteFilter(nameof(IEntity.IsDeleted));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
