using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Availability.Responses;

namespace Scheduling.Application.Availability.Queries;

public sealed record GetAvailabilityQuery(
    Guid ProfessionalId,
    DateOnly Date,
    Guid ServiceId
);


