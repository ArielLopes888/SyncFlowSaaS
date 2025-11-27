using Shared.Infrastructure.Events;
using Scheduling.Domain.Events;
using Scheduling.Application.Services;

namespace Scheduling.Infrastructure.Handlers;

public class LoyaltyHandler : IHandle<AppointmentCreated>
{
    private readonly ILoyaltyService _loyalty;

    public LoyaltyHandler(ILoyaltyService loyalty) => _loyalty = loyalty;

    public async Task Handle(AppointmentCreated domainEvent, CancellationToken cancellationToken = default)
    {
        await _loyalty.RegisterVisitAndTriggerRewards(domainEvent.AppointmentId, cancellationToken);
    }
}
