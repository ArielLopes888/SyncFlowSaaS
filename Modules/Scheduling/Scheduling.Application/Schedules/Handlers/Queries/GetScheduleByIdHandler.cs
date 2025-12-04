using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Queries;

public class GetScheduleByIdHandler : IQueryHandler<GetScheduleByIdQuery, Schedule?>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public GetScheduleByIdHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public Task<Schedule?> HandleAsync(GetScheduleByIdQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        return _repo.GetByIdWithDetailsAsync(tenantId, query.ScheduleId, ct);
    }
}
