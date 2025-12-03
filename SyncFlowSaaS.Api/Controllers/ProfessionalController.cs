using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Professionals.Commands;
using Scheduling.Application.Professionals.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Application.Abstractions;

namespace SyncFlowSaaS.Api.Controllers;

[ApiController]
[Route("api/professionals")]
public class ProfessionalController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public ProfessionalController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfessionalCommand cmd)
    {
        var id = await _dispatcher.SendAsync<CreateProfessionalCommand, Guid>(cmd);
        return Ok(new { id });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfessionalCommand body)
    {
        var cmd = body with { ProfessionalId = id }; 
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _dispatcher.SendAsync(new DeleteProfessionalCommand(id));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _dispatcher.QueryAsync<GetProfessionalByIdQuery, Professional?>(
            new GetProfessionalByIdQuery(id)
        );

        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _dispatcher.QueryAsync<ListProfessionalsQuery, List<Professional>>(
            new ListProfessionalsQuery()
        );

        return Ok(result);
    }
}

