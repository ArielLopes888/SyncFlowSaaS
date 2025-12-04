using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class RemoveScheduleOverrideHandler : ICommandHandler<RemoveScheduleOverrideCommand>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public RemoveScheduleOverrideHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task HandleAsync(RemoveScheduleOverrideCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var schedule = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.ScheduleId, ct)
            ?? throw new KeyNotFoundException("Schedule not found.");

        schedule.RemoveOverride(cmd.OverrideId);

        await _repo.UpdateAsync(schedule, ct);
    }
}
