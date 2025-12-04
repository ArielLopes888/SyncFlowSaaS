using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class AddTimeOffHandler : ICommandHandler<AddTimeOffCommand, Guid>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public AddTimeOffHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task<Guid> HandleAsync(AddTimeOffCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var schedule = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.ScheduleId, ct)
            ?? throw new KeyNotFoundException("Schedule not found.");

        var to = schedule.AddTimeOff(cmd.StartDate, cmd.EndDate);

        await _repo.UpdateAsync(schedule, ct);

        return to.Id;
    }
}
