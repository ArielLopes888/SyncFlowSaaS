using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Abstractions;
using Scheduling.Application.Services.Commands;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;

namespace SyncFlowSaaS.Api.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public ServiceController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCommand cmd)
    {
        
        var id = await _dispatcher.SendAsync<CreateServiceCommand, Guid>(cmd);
        return Ok(new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCommand body)
    {
        var cmd = body with { ServiceId = id }; 
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> Toggle(Guid id, [FromBody] ToggleServiceRequest request)
    {
        var cmd = new ToggleServiceStatusCommand(id, request.Active);
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _dispatcher.QueryAsync<GetServiceByIdQuery, Service?>(
            new GetServiceByIdQuery(id) 
        );

        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _dispatcher.QueryAsync<ListServicesQuery, List<Service>>(
            new ListServicesQuery() 
        );

        return Ok(result);
    }
}