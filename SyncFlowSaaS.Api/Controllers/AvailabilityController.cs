// SyncFlowSaaS.Api.Controllers/AvailabilityController.cs
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Abstractions;
using Scheduling.Application.Availability.Queries;
using Scheduling.Application.Availability.Responses;

namespace SyncFlowSaaS.Api.Controllers;

[ApiController]
[Route("api/availability")]
public class AvailabilityController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public AvailabilityController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    // GET /api/availability?professionalId={id}&serviceId={id}&date=2025-12-01
    [HttpGet]
    public async Task<IActionResult> Get(
    [FromQuery] Guid professionalId,
    [FromQuery] Guid serviceId,
    [FromQuery] string date)
    {
        if (!DateOnly.TryParse(date, out var dateOnly))
            return BadRequest("Formato de data inválido. Use yyyy-MM-dd.");

        var query = new GetAvailabilityQuery(professionalId, dateOnly,serviceId);

        var slots = await _dispatcher
            .QueryAsync<GetAvailabilityQuery, List<TimeSlot>>(query);

        return Ok(slots);
    }

}
