using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class RemoveTimeOffHandler : ICommandHandler<RemoveTimeOffCommand>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public RemoveTimeOffHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task HandleAsync(RemoveTimeOffCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var schedule = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.ScheduleId, ct)
            ?? throw new KeyNotFoundException("Schedule not found.");

        schedule.RemoveTimeOff(cmd.TimeOffId);

        await _repo.UpdateAsync(schedule, ct);
    }
}
