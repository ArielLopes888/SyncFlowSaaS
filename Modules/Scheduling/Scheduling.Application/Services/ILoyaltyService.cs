

namespace Scheduling.Application.Services
{
    public interface ILoyaltyService
    {
        Task RegisterVisitAndTriggerRewards(Guid appointmentId, CancellationToken ct = default);
    }
}
