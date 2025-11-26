using Scheduling.Application.Services;

namespace Scheduling.Infrastructure.Services
{
    public class LoyaltyService : ILoyaltyService
    {
        public Task RegisterVisitAndTriggerRewards(Guid appointmentId, CancellationToken ct = default)
        {
            Console.WriteLine($"[Loyalty] Registering visit for appointment {appointmentId}");
            return Task.CompletedTask;
        }
    }
}
