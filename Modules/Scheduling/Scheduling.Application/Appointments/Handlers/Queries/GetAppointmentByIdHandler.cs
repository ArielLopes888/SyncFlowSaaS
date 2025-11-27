using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Appointments.Handlers.Queries;

public class GetAppointmentByIdHandler : IQueryHandler<GetAppointmentByIdQuery, Appointment?>
{
    private readonly IAppointmentRepository _repo;
    private readonly ITenantProvider _tenantProvider;

    public GetAppointmentByIdHandler(
        IAppointmentRepository repo,
        ITenantProvider tenant
        )
    {
        _repo = repo;
        _tenantProvider = tenant;
    }

    public Task<Appointment?> HandleAsync(GetAppointmentByIdQuery query, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();
        return _repo.GetByIdWithDetailsAsync(tenantId, query.AppointmentId, ct);
    }
}
