namespace Scheduling.Application.Appointments.Commands;

public record CreateAppointmentCommand(
    Guid ProfessionalId,
    Guid ServiceId,
    DateTime StartAt,
    DateTime EndAt,
    string ClientName,
    string ClientPhone
);

