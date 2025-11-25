using Shared.Core.Domain;
using Scheduling.Domain.Enums;
using Scheduling.Domain.Events;
using Shared.Core.Abstractions;

namespace Scheduling.Domain.Entities;

public class Appointment : BaseEntity, IAggregateRoot
{
    public Guid ProfessionalId { get; private set; }
    public Guid ServiceId { get; private set; }
    public DateTime Date { get; private set; }
    public string ClientName { get; private set; }
    public string? Phone { get; private set; }
    public AppointmentStatus Status { get; private set; }

    private Appointment() { }

    public Appointment(Guid professionalId, Guid serviceId, DateTime date, string clientName, string? phone = null)
    {
        ProfessionalId = professionalId;
        ServiceId = serviceId;
        Date = date;
        ClientName = clientName;
        Phone = phone;
        Status = AppointmentStatus.Created;

        AddDomainEvent(new AppointmentCreated(Id));
    }

    public void Cancel()
    {
        Status = AppointmentStatus.Canceled;
        AddDomainEvent(new AppointmentCanceled(Id));
    }
}
