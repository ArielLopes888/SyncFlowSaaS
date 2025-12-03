using Microsoft.Extensions.DependencyInjection;
using Scheduling.Application.Abstractions;
using System.Reflection;
using Scrutor;
using Scheduling.Application.Abstractions.Handlers;

namespace Scheduling.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services, Assembly applicationAssembly)
        {
            services.AddScoped<IDispatcher, Dispatcher>();

            // Registrando handlers automaticamente
            services.Scan(scan => scan
                .FromAssemblies(applicationAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(applicationAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }
    }
}
