using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions; 

namespace Scheduling.Application.Appointments.Handlers.Commands;

public class UpdateAppointmentHandler : ICommandHandler<UpdateAppointmentCommand>
{
    private readonly IAppointmentRepository _repo;

    public UpdateAppointmentHandler(IAppointmentRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(UpdateAppointmentCommand cmd, CancellationToken ct = default)
    {
        var appointment = await _repo.GetByIdWithDetailsAsync(cmd.TenantId, cmd.AppointmentId, ct)
            ?? throw new AppointmentNotFoundException(cmd.AppointmentId); 

        appointment.Update(cmd.StartAt, cmd.EndAt, cmd.ClientName, cmd.ClientPhone);

        await _repo.UpdateAsync(appointment, ct);
    }
}