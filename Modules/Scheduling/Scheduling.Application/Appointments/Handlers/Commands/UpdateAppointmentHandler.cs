using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Shared.Core.Providers;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Appointments.Handlers.Commands;

public class UpdateAppointmentHandler : ICommandHandler<UpdateAppointmentCommand>
{
    private readonly IAppointmentRepository _repo;
    private readonly ITenantProvider _tenantProvider;

    public UpdateAppointmentHandler(
        IAppointmentRepository repo,
        ITenantProvider tenantProvider)
    {
        _repo = repo;
        _tenantProvider = tenantProvider;
    }

    public async Task HandleAsync(UpdateAppointmentCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();

        var appointment = await _repo.GetByIdWithDetailsAsync(
            tenantId,
            cmd.AppointmentId,
            ct)
            ?? throw new AppointmentNotFoundException(cmd.AppointmentId);

        appointment.Update(cmd.StartAt, cmd.EndAt, cmd.ClientName, cmd.ClientPhone);

        await _repo.UpdateAsync(appointment, ct);
    }
}
