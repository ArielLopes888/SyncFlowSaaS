namespace Scheduling.Application.Schedules.Commands;

public record RemoveTimeOffCommand(
    Guid ScheduleId,
    Guid TimeOffId
);
