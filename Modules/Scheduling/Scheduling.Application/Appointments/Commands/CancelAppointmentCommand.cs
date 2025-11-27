namespace Scheduling.Application.Appointments.Commands;

public record CancelAppointmentCommand(
    Guid AppointmentId
);
