using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class CreateScheduleHandler : ICommandHandler<CreateScheduleCommand, Guid>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public CreateScheduleHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task<Guid> HandleAsync(CreateScheduleCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();

        var schedule = new Schedule(
            tenantId,
            cmd.ProfessionalId,
            cmd.Day,
            cmd.Start,
            cmd.End,
            cmd.IntervalMinutes
        );

        await _repo.AddAsync(schedule, ct);

        return schedule.Id;
    }
}
