using Application.Common.Behaviours;
using Application.Common.MediatR;
using Application.Models.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
                cfg.NotificationPublisher = new CustomPublisher(); // this will be singleton
                cfg.NotificationPublisherType = typeof(CustomPublisher); // this will be the ServiceLifetime
            });
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)))
             .AddScoped(cnf => cnf.GetService<IOptionsSnapshot<ApplicationSettings>>().Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return services;
        }
    }
}
