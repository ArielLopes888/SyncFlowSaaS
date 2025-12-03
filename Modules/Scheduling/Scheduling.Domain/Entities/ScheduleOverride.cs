using Shared.Core.Domain;
using Shared.Core.Exceptions;

namespace Scheduling.Domain.Entities;

public class ScheduleOverride : BaseEntity
{
    public Guid ScheduleId { get; private set; }
    public DateTime Date { get; private set; }

    public TimeSpan? Start { get; private set; }
    public TimeSpan? End { get; private set; }
    public bool IsClosed { get; private set; }

    private ScheduleOverride() { }

    public ScheduleOverride(
        Guid scheduleId,
        DateTime date,
        TimeSpan? start,
        TimeSpan? end,
        bool isClosed)
    {
        ScheduleId = scheduleId;
        Date = date.Date; // normalize

        Start = start;
        End = end;
        IsClosed = isClosed;

        Validate();
    }

    private void Validate()
    {
        if (!IsClosed && Start.HasValue && End.HasValue && Start >= End)
            throw new DomainException("Override start must be before end.");
    }
}
