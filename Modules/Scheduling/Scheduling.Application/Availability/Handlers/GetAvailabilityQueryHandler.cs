using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments;
using Scheduling.Application.Availability.Queries;
using Scheduling.Application.Availability.Responses;
using Scheduling.Domain.Repositories;

namespace Scheduling.Application.Availability.Handlers
{
    public class GetAvailabilityHandler : IQueryHandler<GetAvailabilityQuery, List<TimeSlot>>
    {
        private readonly IAvailabilityService _availabilityService;

        public GetAvailabilityHandler(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        public async Task<List<TimeSlot>> HandleAsync(GetAvailabilityQuery query, CancellationToken ct = default)
        {
            return await _availabilityService.GetDailyAvailabilityAsync(
                query.ProfessionalId,
                query.Date,
                query.ServiceId,
                ct
            );
        }
    }
}
