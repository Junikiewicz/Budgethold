using Budgethold.Application.Contracts.Persistance;
using Budgethold.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Budgethold.Persistance.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseConnection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddIdentityStores(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>();
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();
        }
    }
}
