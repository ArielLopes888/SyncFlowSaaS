using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.Domain.Repositories;
using Scheduling.Infrastructure.Handlers;
using Scheduling.Infrastructure.Persistence; 
using Scheduling.Infrastructure.Repositories;
using Shared.Infrastructure.Events;

namespace Scheduling.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSchedulingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 👇 ADICIONAR REGISTRO DO DBCONTEXT
        services.AddDbContext<SchedulingDbContext>(options =>
        {
            var inMemory = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (inMemory)
                options.UseInMemoryDatabase("scheduling-db");
            else
                options.UseSqlServer(configuration.GetConnectionString("SchedulingConnection"));
        });

        // Repositórios
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        // Application services
        services.AddScoped<IHandle<AppointmentCreated>, NotifyProfessionalHandler>();
        services.AddScoped<IHandle<AppointmentCreated>, UpdateAnalyticsHandler>();
        services.AddScoped<IHandle<AppointmentCreated>, LoyaltyHandler>();
        services.AddScoped<IHandle<Scheduling.Domain.Events.AppointmentCanceled>, NotifyCancelHandler>();

        return services;
    }
}