using Shared.Core.Domain;
using Scheduling.Domain.Enums;
using Scheduling.Domain.Events;
using Shared.Core.Abstractions;

namespace Scheduling.Domain.Entities;

public class Appointment : BaseEntity, IAggregateRoot
{

    public Guid TenantId { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public Guid ServiceId { get; private set; }

    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }

    public string ClientName { get; private set; }
    public string? Phone { get; private set; }

    public AppointmentStatus Status { get; private set; }

    private Appointment() { }

    public Appointment(
        Guid tenantId, 
        Guid professionalId,
        Guid serviceId,
        DateTime startAt,
        DateTime endAt,
        string clientName,
        string? phone = null)
    {
        TenantId = tenantId;
        ProfessionalId = professionalId;
        ServiceId = serviceId;

        StartAt = startAt;
        EndAt = endAt;

        ClientName = clientName ?? throw new ArgumentNullException(nameof(clientName));
        Phone = phone;

        Status = AppointmentStatus.Created;

        AddDomainEvent(new AppointmentCanceled(this.Id, this.TenantId));

    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Canceled) return;

        Status = AppointmentStatus.Canceled;
        AddDomainEvent(new AppointmentCanceled(this.Id, this.TenantId));

    }
}
