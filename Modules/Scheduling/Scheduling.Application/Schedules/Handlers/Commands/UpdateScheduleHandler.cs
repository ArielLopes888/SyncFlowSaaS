using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class UpdateScheduleHandler : ICommandHandler<UpdateScheduleCommand>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public UpdateScheduleHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task HandleAsync(UpdateScheduleCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var schedule = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.ScheduleId, ct)
            ?? throw new KeyNotFoundException("Schedule not found.");

        schedule.Update(cmd.Start, cmd.End, cmd.IntervalMinutes);

        await _repo.UpdateAsync(schedule, ct);
    }
}
