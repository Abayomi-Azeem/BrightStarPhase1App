using BrightStarPhase1App.Data;
using BrightStarPhase1App.PipelineBehaviours;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BrightStarPhase1App.Filters
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
                .EnableDetailedErrors(true), ServiceLifetime.Scoped);
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                x.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddExceptionHandler<GlobalExceptionHandler>();
            //services.AddProblemDetails();
            return services;
        }
    }
}
