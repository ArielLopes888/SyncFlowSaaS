using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Commands;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Appointments.Handlers.Commands;

public class CreateAppointmentHandler : ICommandHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repo;
    private readonly IServiceRepository _services;
    private readonly IProfessionalRepository _professionals;
    private readonly ITenantProvider _tenantProvider;

    public CreateAppointmentHandler(
        IAppointmentRepository repo,
        IServiceRepository services,
        IProfessionalRepository professionals,
        ITenantProvider tenant
        )
    {
        _repo = repo;
        _services = services;
        _professionals = professionals;
        _tenantProvider = tenant;
    }

    public async Task<Guid> HandleAsync(CreateAppointmentCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenantProvider.GetTenantId();
        var professional = await _professionals.GetByIdAsync(cmd.ProfessionalId, ct)
            ?? throw new ProfessionalNotFoundException(cmd.ProfessionalId); 

        var service = await _services.GetByIdAsync(tenantId, cmd.ServiceId, ct)
            ?? throw new ServiceNotFoundException(cmd.ServiceId);

        var conflict = await _repo.ExistsConflictingAsync(
            tenantId,
            cmd.ProfessionalId,
            cmd.StartAt,
            cmd.EndAt,
            ct
        );

        if (conflict)
            throw new AppointmentConflictException(cmd.ProfessionalId, cmd.StartAt); 

        var appointment = new Appointment(
            tenantId,
            cmd.ProfessionalId,
            cmd.ServiceId,
            cmd.StartAt,
            cmd.EndAt,
            cmd.ClientName,
            cmd.ClientPhone
        );

        await _repo.AddAsync(appointment, ct);

        return appointment.Id;
    }
}