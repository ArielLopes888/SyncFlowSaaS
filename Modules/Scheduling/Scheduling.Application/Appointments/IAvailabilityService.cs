using Scheduling.Application.Availability.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.Appointments
{

    public interface IAvailabilityService
    {
        Task<List<TimeSlot>> GetDailyAvailabilityAsync(Guid professionalId, DateOnly date, Guid serviceId, CancellationToken ct = default);
    }

}
