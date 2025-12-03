using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Professionals.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Handlers.Queries;

public class GetProfessionalByIdHandler
    : IQueryHandler<GetProfessionalByIdQuery, Professional?>
{
    private readonly IProfessionalRepository _repo;
    private readonly ITenantProvider _tenant;

    public GetProfessionalByIdHandler(IProfessionalRepository repo, ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public Task<Professional?> HandleAsync(GetProfessionalByIdQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        return _repo.GetByIdAsync(tenantId, query.ProfessionalId, ct);
    }
}
