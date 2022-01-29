using Budgethold.API.Extensions;
using Budgethold.Application.Commands.Wallet.AddWallet;
using Budgethold.Persistance;
using Budgethold.Persistance.Extensions;
using Budgethold.Security.Commands.SignUp;
using Budgethold.Security.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseConnection(Configuration.GetSection("Database:ConnectionString").Value);
            services.AddIdentityStores();
            services.AddCookieAuthentication();

            services.AddValidation();

            services.AddMediatR(typeof(SignUpCommand), typeof(AddWalletCommand));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Budgethold");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
