using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Commands;

public class DeleteScheduleHandler : ICommandHandler<DeleteScheduleCommand>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public DeleteScheduleHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task HandleAsync(DeleteScheduleCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var schedule = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.ScheduleId, ct)
            ?? throw new KeyNotFoundException("Schedule not found.");

        await _repo.DeleteAsync(schedule, ct);
    }
}
