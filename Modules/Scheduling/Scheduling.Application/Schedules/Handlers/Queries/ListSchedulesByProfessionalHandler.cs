using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Schedules.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Schedules.Handlers.Queries;

public class ListSchedulesByProfessionalHandler : IQueryHandler<ListSchedulesByProfessionalQuery, List<Schedule>>
{
    private readonly IScheduleRepository _repo;
    private readonly ITenantProvider _tenant;

    public ListSchedulesByProfessionalHandler(IScheduleRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public Task<List<Schedule>> HandleAsync(ListSchedulesByProfessionalQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        return _repo.GetByProfessionalAsync(tenantId, query.ProfessionalId, ct);
    }
}
