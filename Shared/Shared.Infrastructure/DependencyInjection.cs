using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Providers;
using Shared.Core.Repositories;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Events;
using Shared.Infrastructure.Observability;
using Shared.Infrastructure.Persistence;
using Shared.Infrastructure.Providers;
using Shared.Infrastructure.Repositories;
using Shared.Infrastructure.Tenancy;

namespace Shared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // HttpContext for TenantProvider
        services.AddHttpContextAccessor();

        // Tenant Provider
        services.AddScoped<ITenantProvider, TenantProvider>();

        // Providers
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IGuidProvider, GuidProvider>();

        // DbContext do Shared
        services.AddDbContext<AppDbContext>(options =>
        {
            var inMemory = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (inMemory)
                options.UseInMemoryDatabase("dev-db");
            else
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });


        // Unit of Work shared
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        // Domain Events
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // Observability
        services.Configure<ObservabilityOptions>(configuration.GetSection("Observability"));

        return services;
    }
}
