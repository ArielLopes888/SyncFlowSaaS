
namespace Scheduling.Application.Services
{
    public interface IAnalyticsService
    {
        Task TrackAppointmentCreatedAsync(Guid appointmentId, CancellationToken ct = default);
        Task TrackAppointmentCanceledAsync(Guid appointmentId, CancellationToken ct = default);
    }
}
