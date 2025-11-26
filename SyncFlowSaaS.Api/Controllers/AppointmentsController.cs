using Microsoft.AspNetCore.Mvc;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;
using Shared.Core.Repositories;
using SyncFlowSaaS.Api.Controllers;

namespace Scheduling.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IServiceRepository _serviceRepo;
    private readonly IUnitOfWork _uow;

    public AppointmentsController(
        IAppointmentRepository appointmentRepo,
        IServiceRepository serviceRepo,
        IUnitOfWork uow)
    {
        _appointmentRepo = appointmentRepo;
        _serviceRepo = serviceRepo;
        _uow = uow;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request, CancellationToken ct)
    {
        if (request.StartAt >= request.EndAt)
            return BadRequest("StartAt must be before EndAt.");
       
        var service = await _serviceRepo.GetByIdAsync(request.TenantId, request.ServiceId, ct);
        if (service == null)
            return BadRequest("Service not found for this tenant.");

        var conflict = await _appointmentRepo.ExistsConflictingAsync(request.TenantId, request.ProfessionalId, request.StartAt, request.EndAt, ct);
        if (conflict) return Conflict("This professional already has an appointment in this time range.");

        var appointment = new Appointment(
            request.TenantId,
            request.ProfessionalId,
            request.ServiceId,
            request.StartAt,
            request.EndAt,
            request.ClientName,
            request.ClientPhone);

        await _appointmentRepo.AddAsync(appointment, ct);
        await _uow.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { tenantId = request.TenantId, id = appointment.Id }, new { appointment.Id });
    }

    [HttpGet("{tenantId:guid}/{id:guid}")]
    public async Task<IActionResult> GetById(Guid tenantId, Guid id, CancellationToken ct)
    {
        var appointment = await _appointmentRepo.GetByIdWithDetailsAsync(tenantId, id, ct);
        if (appointment == null) return NotFound();
        return Ok(appointment);
    }
}
