using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.Domain.Events;
using Scheduling.Domain.Repositories;
using Scheduling.Infrastructure.Handlers;
using Scheduling.Infrastructure.Persistence;
using Scheduling.Infrastructure.Repositories;
using Shared.Infrastructure.Events;

namespace Scheduling.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSchedulingInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext específico do Scheduling
        services.AddDbContext<SchedulingDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("SchedulingConnection"),
                sql =>
                {
                    sql.MigrationsAssembly(typeof(SchedulingDbContext).Assembly.FullName);
                }));

        // Repositórios
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        // Domain Events – handlers múltiplos para o mesmo evento
        services.AddScoped<IHandle<AppointmentCreated>, NotifyProfessionalHandler>();
        services.AddScoped<IHandle<AppointmentCreated>, UpdateAnalyticsHandler>();
        services.AddScoped<IHandle<AppointmentCreated>, LoyaltyHandler>();
        services.AddScoped<IHandle<AppointmentCanceled>, NotifyCancelHandler>();

        return services;
    }
}
