namespace Scheduling.Application.Schedules.Commands;

// Adiciona uma override ao schedule (Replace/Add/Remove semantics são tratadas no agregado)
public record AddScheduleOverrideCommand(
    Guid ScheduleId,
    DateOnly Date,
    TimeSpan? Start,
    TimeSpan? End,
    bool IsClosed
);
