using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;


namespace Scheduling.Application.Appointments.Handlers;

public class ListAppointmentsHandler : IQueryHandler<ListAppointmentsQuery, List<Appointment>>
{
    private readonly IAppointmentRepository _repo;
    private readonly ITenantProvider _tenantProvider;
    public ListAppointmentsHandler(IAppointmentRepository repo,
        ITenantProvider tenant
        )
    {
        _repo = repo;
        _tenantProvider = tenant;
    }

    public async Task<List<Appointment>> HandleAsync(ListAppointmentsQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();
        return await _repo.GetByTenantAsync(tenantId, ct);
    }
}
