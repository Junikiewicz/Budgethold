using Budgethold.API.Common.Validation;
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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //AssemblyScanner.FindValidatorsInAssembly(typeof(AddRewardCommandValidator).Assembly).ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            services.AddMediatR(typeof(SignUpCommand));

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
                app.UseSwaggerUI();
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
