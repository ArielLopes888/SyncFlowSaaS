using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Professionals.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Handlers.Queries;

public class ListProfessionalsHandler
    : IQueryHandler<ListProfessionalsQuery, List<Professional>>
{
    private readonly IProfessionalRepository _repo;
    private readonly ITenantProvider _tenant;

    public ListProfessionalsHandler(IProfessionalRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public Task<List<Professional>> HandleAsync(ListProfessionalsQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        return _repo.GetByTenantAsync(tenantId, ct);
    }
}
