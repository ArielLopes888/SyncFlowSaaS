namespace Scheduling.Application.Appointments.Queries;

public record ListAppointmentsByProfessionalQuery(
    Guid ProfessionalId,
    DateTime From,
    DateTime To
);
