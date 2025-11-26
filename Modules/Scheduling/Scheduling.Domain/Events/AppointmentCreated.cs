using Shared.Core.Domain;

public class AppointmentCreated : DomainEvent
{
    public Guid AppointmentId { get; }
    public Guid TenantId { get; }
    public Guid ProfessionalId { get; }
    public string ClientName { get; }

    public AppointmentCreated(Guid appointmentId, Guid tenantId, Guid professionalId, string clientName)
    {
        AppointmentId = appointmentId;
        TenantId = tenantId;
        ProfessionalId = professionalId;
        ClientName = clientName;
    }
}
