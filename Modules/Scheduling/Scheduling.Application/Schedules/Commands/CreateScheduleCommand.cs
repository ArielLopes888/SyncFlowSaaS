namespace Scheduling.Application.Schedules.Commands;

public record CreateScheduleCommand(
    Guid ProfessionalId,
    DayOfWeek Day,
    TimeSpan Start,
    TimeSpan End,
    int IntervalMinutes
);
