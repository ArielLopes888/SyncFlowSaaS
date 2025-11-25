using Shared.Core.Domain;

namespace Scheduling.Domain.Events
{
    public record AppointmentCreated(Guid AppointmentId) : DomainEvent;
}
