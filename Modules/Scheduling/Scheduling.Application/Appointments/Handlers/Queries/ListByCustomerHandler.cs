using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;
using Shared.Core.Providers;

namespace Scheduling.Application.Appointments.Handlers.Queries;

public class ListByCustomerHandler : IQueryHandler<ListAppointmentsByCustomerQuery, List<Appointment>>
{
    private readonly IAppointmentRepository _repo;
    private readonly ITenantProvider _tenantProvider;

    public ListByCustomerHandler(
        IAppointmentRepository repo,
        ITenantProvider tenantProvider)
    {
        _repo = repo;
        _tenantProvider = tenantProvider;
    }

    public Task<List<Appointment>> HandleAsync(ListAppointmentsByCustomerQuery _, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();

        return _repo.GetByCustomerAsync(
            tenantId,
            _.ClientName,
            ct
        );
    }
}
