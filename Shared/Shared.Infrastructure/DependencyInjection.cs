using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Providers;
using Shared.Core.Repositories;
using Shared.Infrastructure.Events;
using Shared.Infrastructure.Observability;
using Shared.Infrastructure.Persistence;
using Shared.Infrastructure.Providers;
using Shared.Infrastructure.Repositories;

namespace Shared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Providers
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IGuidProvider, GuidProvider>();

        // DbContext - use InMemory by default for dev; actual DB provider configured in API
        services.AddDbContext<AppDbContext>(options =>
        {
            var inMemory = configuration.GetValue<bool>("UseInMemoryDatabase");
            if (inMemory)
                options.UseInMemoryDatabase("dev-db");
            else
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        // Repositories generic registrations
        services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        // Unit of Work
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        // Event dispatcher
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // Observability options
        services.Configure<ObservabilityOptions>(configuration.GetSection("Observability"));

        return services;
    }
}
