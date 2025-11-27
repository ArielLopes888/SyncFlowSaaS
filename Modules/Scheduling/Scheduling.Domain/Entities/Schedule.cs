using Shared.Core.Domain;
using Shared.Core.Abstractions;

namespace Scheduling.Domain.Entities;

public class Schedule : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public DayOfWeek Day { get; private set; }
    public TimeSpan Start { get; private set; }
    public TimeSpan End { get; private set; }
    public int IntervalMinutes { get; private set; }

    private Schedule() { }

    public Schedule(Guid tenantId, Guid professionalId, DayOfWeek day,
                       TimeSpan start, TimeSpan end, int intervalMinutes)
    {
        TenantId = tenantId;
        ProfessionalId = professionalId;
        Day = day;
        Start = start;
        End = end;
        IntervalMinutes = intervalMinutes;
    }
}
