namespace Scheduling.Application.Appointments.Commands;

public record UpdateAppointmentCommand(
    Guid AppointmentId,
    DateTime StartAt,
    DateTime EndAt,
    string ClientName,
    string ClientPhone
);
