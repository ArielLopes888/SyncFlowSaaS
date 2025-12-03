using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Abstractions;
using Scheduling.Application.Appointments.Commands;
using Scheduling.Application.Appointments.Queries;
using Scheduling.Domain.Entities;

namespace SyncFlowSaaS.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public AppointmentsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand cmd)
    {
        var id = await _dispatcher.SendAsync<CreateAppointmentCommand, Guid>(cmd);
        return Ok(new { id });
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var cmd = new CancelAppointmentCommand(id); 
        await _dispatcher.SendAsync(cmd);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetAppointmentByIdQuery(id); 

        var result = await _dispatcher.QueryAsync<GetAppointmentByIdQuery, Appointment?>(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _dispatcher.QueryAsync<ListAppointmentsQuery, List<Appointment>>(
            new ListAppointmentsQuery()
        );
        return Ok(result);
    }

    [HttpGet("professional/{professionalId}")]
    public async Task<IActionResult> ListByProfessional(Guid professionalId, DateTime from, DateTime to)
    {
        var result = await _dispatcher.QueryAsync<ListAppointmentsByProfessionalQuery, List<Appointment>>(
            new ListAppointmentsByProfessionalQuery(professionalId, from, to)
        );
        return Ok(result);
    }

    [HttpGet("customer")]
    public async Task<IActionResult> ListByCustomer([FromQuery] string clientName)
    {
        var result = await _dispatcher.QueryAsync<ListAppointmentsByCustomerQuery, List<Appointment>>(
            new ListAppointmentsByCustomerQuery(clientName) 
        );
        return Ok(result);
    }
}