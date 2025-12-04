using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class AddScheduleOverrideHandler : ICommandHandler<AddScheduleOverrideCommand, Guid>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public AddScheduleOverrideHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task<Guid> HandleAsync(AddScheduleOverrideCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var schedule = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.ScheduleId, ct)
            ?? throw new KeyNotFoundException("Schedule not found.");

        var date = cmd.Date.ToDateTime(TimeOnly.MinValue);
        var ov = schedule.AddOverride(date, cmd.Start, cmd.End, cmd.IsClosed);

        await _repo.UpdateAsync(schedule, ct);

        return ov.Id;
    }
}
