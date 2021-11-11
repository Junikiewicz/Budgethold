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
            var builder = services.AddIdentityCore<AspNetUser>();
            builder = new IdentityBuilder(builder.UserType, typeof(AspNetRole), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<AspNetRole>>();
            builder.AddRoleManager<RoleManager<AspNetRole>>();
            builder.AddSignInManager<SignInManager<AspNetUser>>();
            builder.AddDefaultTokenProviders();
        }
    }
}
