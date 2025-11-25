using Shared.Core.Domain;
using Shared.Core.Abstractions;

namespace Scheduling.Domain.Entities;

public class Schedule : BaseEntity
{
    public DayOfWeek Day { get; private set; }
    public TimeSpan Start { get; private set; }
    public TimeSpan End { get; private set; }
    public int IntervalMinutes { get; private set; }

    private Schedule() { }

    public Schedule(DayOfWeek day, TimeSpan start, TimeSpan end, int intervalMinutes)
    {
        Day = day;
        Start = start;
        End = end;
        IntervalMinutes = intervalMinutes;
    }
}
