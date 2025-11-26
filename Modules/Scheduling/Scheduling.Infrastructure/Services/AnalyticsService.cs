using Scheduling.Application.Services;

namespace Scheduling.Infrastructure.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public Task TrackAppointmentCreatedAsync(Guid appointmentId, CancellationToken ct = default)
        {
            Console.WriteLine($"[Analytics] Appointment created => {appointmentId}");
            return Task.CompletedTask;
        }

        public Task TrackAppointmentCanceledAsync(Guid appointmentId, CancellationToken ct = default)
        {
            Console.WriteLine($"[Analytics] Appointment canceled => {appointmentId}");
            return Task.CompletedTask;
        }
    }
}
