namespace Scheduling.Application.Schedules.Commands;

public record AddTimeOffCommand(
    Guid ScheduleId,
    DateTime StartDate,
    DateTime EndDate
);
