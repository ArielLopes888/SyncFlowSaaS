namespace Scheduling.Application.Appointments.Queries;

public record ListByProfessionalQuery(
    Guid ProfessionalId,
    DateTime From,
    DateTime To
);
