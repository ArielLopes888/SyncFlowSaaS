using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.Domain.Repositories;
using Scheduling.Infrastructure.Repositories;
using Scheduling.Application.Handlers;
using Shared.Infrastructure.Events;

namespace Scheduling.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSchedulingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositórios
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        // Application services (contracts in Scheduling.Application)
        // services.AddScoped<INotificationService, NotificationService>(); // impl in infra
        // services.AddScoped<IAnalyticsService, AnalyticsService>();

        // Register domain event handlers
        services.AddScoped<IHandle<AppointmentCreated>, NotifyProfessionalHandler>();
        services.AddScoped<IHandle<AppointmentCreated>, UpdateAnalyticsHandler>();
        services.AddScoped<IHandle<AppointmentCreated>, LoyaltyHandler>();

        services.AddScoped<IHandle<Scheduling.Domain.Events.AppointmentCanceled>, /* Handler for cancel */ NotifyCancelHandler>();

        return services;
    }
}
