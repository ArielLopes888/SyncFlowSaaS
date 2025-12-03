using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Appointments.Handlers;

public class ListByProfessionalHandler : IQueryHandler<ListAppointmentsByProfessionalQuery, List<Appointment>>
{
    private readonly IAppointmentRepository _repo;
    private readonly ITenantProvider _tenantProvider;
    public ListByProfessionalHandler(
        IAppointmentRepository repo,
        ITenantProvider tenant
        )
    {
        _repo = repo;
        _tenantProvider = tenant;
    }

    public Task<List<Appointment>> HandleAsync(ListAppointmentsByProfessionalQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();
        return _repo.GetByProfessionalAndRangeAsync(
            tenantId,
            query.ProfessionalId,
            query.From,
            query.To,
            ct
        );
    }
}
