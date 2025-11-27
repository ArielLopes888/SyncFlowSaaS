using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Appointments.Handlers.Commands;

public class CancelAppointmentHandler : ICommandHandler<CancelAppointmentCommand>
{
    private readonly IAppointmentRepository _repo;
    private readonly ITenantProvider _tenantProvider;

    public CancelAppointmentHandler(
        IAppointmentRepository repo,
        ITenantProvider tenant
        )
    {
        _repo = repo;
        _tenantProvider = tenant;
    }

    public async Task HandleAsync(CancelAppointmentCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();
        var appointment = await _repo.GetByIdWithDetailsAsync(tenantId, cmd.AppointmentId, ct)
            ?? throw new AppointmentNotFoundException(cmd.AppointmentId); 

        appointment.Cancel();

        await _repo.UpdateAsync(appointment, ct);
    }
}