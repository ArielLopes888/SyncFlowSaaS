using Shared.Infrastructure.Events;
using Scheduling.Domain.Events;
using Scheduling.Application.Services;
using Scheduling.Domain.Repositories;

namespace Scheduling.Infrastructure.Handlers;

public class UpdateAnalyticsHandler : IHandle<AppointmentCreated>
{
    private readonly IAppointmentRepository _repo;
    private readonly IAnalyticsService _analytics;

    public UpdateAnalyticsHandler(IAppointmentRepository repo, IAnalyticsService analytics)
    {
        _repo = repo;
        _analytics = analytics;
    }

    public async Task Handle(AppointmentCreated notification, CancellationToken ct)
    {
        var appt = await _repo.GetByIdWithDetailsAsync(notification.TenantId, notification.AppointmentId, ct);
        if (appt == null) return;

        await _analytics.TrackAppointmentCreatedAsync(notification.AppointmentId, ct);
        // if analytics needs tenant context, pass notification.TenantId
    }
}

