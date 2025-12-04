using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Schedules.Commands;
using Scheduling.Application.Schedules.Queries;
using Scheduling.Application.Abstractions;

namespace SyncFlowSaaS.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class ScheduleController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public ScheduleController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScheduleCommand cmd)
    {
        var id = await _dispatcher.SendAsync<CreateScheduleCommand, Guid>(cmd);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScheduleCommand body)
    {
        var cmd = body with { ScheduleId = id };
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cmd = new DeleteScheduleCommand(id);
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetScheduleByIdQuery(id);
        var result = await _dispatcher.QueryAsync<GetScheduleByIdQuery, Scheduling.Domain.Entities.Schedule?>(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("professional/{professionalId}")]
    public async Task<IActionResult> ListByProfessional(Guid professionalId)
    {
        var query = new ListSchedulesByProfessionalQuery(professionalId);
        var result = await _dispatcher.QueryAsync<ListSchedulesByProfessionalQuery, List<Scheduling.Domain.Entities.Schedule>>(query);
        return Ok(result);
    }

    // Overrides / TimeOff endpoints operated via Schedule aggregate
    [HttpPost("{id}/overrides")]
    public async Task<IActionResult> AddOverride(Guid id, [FromBody] AddScheduleOverrideCommand body)
    {
        var cmd = body with { ScheduleId = id };
        var ovId = await _dispatcher.SendAsync<AddScheduleOverrideCommand, Guid>(cmd);
        return CreatedAtAction(nameof(GetById), new { id }, new { overrideId = ovId });
    }

    [HttpDelete("{id}/overrides/{overrideId}")]
    public async Task<IActionResult> RemoveOverride(Guid id, Guid overrideId)
    {
        var cmd = new RemoveScheduleOverrideCommand(id, overrideId);
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpPost("{id}/timeoffs")]
    public async Task<IActionResult> AddTimeOff(Guid id, [FromBody] AddTimeOffCommand body)
    {
        var cmd = body with { ScheduleId = id };
        var toId = await _dispatcher.SendAsync<AddTimeOffCommand, Guid>(cmd);
        return CreatedAtAction(nameof(GetById), new { id }, new { timeOffId = toId });
    }

    [HttpDelete("{id}/timeoffs/{timeOffId}")]
    public async Task<IActionResult> RemoveTimeOff(Guid id, Guid timeOffId)
    {
        var cmd = new RemoveTimeOffCommand(id, timeOffId);
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }
}
