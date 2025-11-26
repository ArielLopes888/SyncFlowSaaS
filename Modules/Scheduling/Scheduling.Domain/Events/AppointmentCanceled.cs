using Shared.Core.Domain;

namespace Scheduling.Domain.Events;

public class AppointmentCanceled : DomainEvent
{
    public Guid AppointmentId { get; }
    public Guid TenantId { get; }

    public AppointmentCanceled(Guid appointmentId, Guid tenantId)
    {
        AppointmentId = appointmentId;
        TenantId = tenantId;
    }
}
