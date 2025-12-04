namespace Scheduling.Application.Schedules.Commands;
public record UpdateScheduleCommand(
    Guid ScheduleId,
    TimeSpan Start,
    TimeSpan End,
    int IntervalMinutes
);
