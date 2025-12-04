namespace Scheduling.Application.Schedules.Commands;

public record RemoveScheduleOverrideCommand(
    Guid ScheduleId,
    Guid OverrideId
);
