using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Budgethold.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options => options.DefaultScheme = IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
