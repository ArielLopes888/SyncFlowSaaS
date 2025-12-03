using Shared.Core.Domain;
using Shared.Core.Exceptions;

namespace Scheduling.Domain.Entities;

public class TimeOff : BaseEntity
{
    public Guid ScheduleId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private TimeOff() { }

    public TimeOff(Guid scheduleId, DateTime startDate, DateTime endDate)
    {
        ScheduleId = scheduleId;
        StartDate = startDate;
        EndDate = endDate;

        if (StartDate >= EndDate)
            throw new DomainException("TimeOff start must be before end.");
    }
}
