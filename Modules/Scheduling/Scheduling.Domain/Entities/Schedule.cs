using Shared.Core.Domain;
using Shared.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Scheduling.Domain.Entities;

public class Schedule : BaseEntity, ITenantEntity, IAggregateRoot
{
    public Guid TenantId { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public DayOfWeek Day { get; private set; }
    public TimeSpan Start { get; private set; }
    public TimeSpan End { get; private set; }
    public int IntervalMinutes { get; private set; }

    private readonly List<ScheduleOverride> _overrides = new();
    public IReadOnlyCollection<ScheduleOverride> Overrides => _overrides.AsReadOnly();

    private readonly List<TimeOff> _timeOffs = new();
    public IReadOnlyCollection<TimeOff> TimeOffs => _timeOffs.AsReadOnly();

    private Schedule() { }

    public Schedule(
        Guid tenantId,
        Guid professionalId,
        DayOfWeek day,
        TimeSpan start,
        TimeSpan end,
        int intervalMinutes)
    {
        TenantId = tenantId;
        ProfessionalId = professionalId;
        Day = day;
        Start = start;
        End = end;
        IntervalMinutes = intervalMinutes;

        Validate();
    }

    public void Update(TimeSpan start, TimeSpan end, int intervalMinutes)
    {
        Start = start;
        End = end;
        IntervalMinutes = intervalMinutes;

        Validate();
    }

    private void Validate()
    {
        if (Start >= End)
            throw new DomainException("Schedule start time must be before end time.");

        if (IntervalMinutes <= 0)
            throw new DomainException("IntervalMinutes must be greater than zero.");
    }

    // -----------------------
    // METHODS TO MANAGE CHILD ENTITIES
    // -----------------------

    public ScheduleOverride AddOverride(DateTime date, TimeSpan? start, TimeSpan? end, bool isClosed)
    {
        var ov = new ScheduleOverride(Id, date, start, end, isClosed);
        _overrides.Add(ov);
        return ov;
    }

    public void RemoveOverride(Guid overrideId)
    {
        var entity = _overrides.FirstOrDefault(x => x.Id == overrideId);
        if (entity != null)
            _overrides.Remove(entity);
    }

    public TimeOff AddTimeOff(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new DomainException("TimeOff start must be before end.");

        var to = new TimeOff(Id, startDate, endDate);
        _timeOffs.Add(to);
        return to;
    }

    public void RemoveTimeOff(Guid timeOffId)
    {
        var entity = _timeOffs.FirstOrDefault(x => x.Id == timeOffId);
        if (entity != null)
            _timeOffs.Remove(entity);
    }
}
