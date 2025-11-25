using Shared.Core.Domain;

namespace Scheduling.Domain.Events
{
    public record AppointmentCanceled(Guid AppointmentId) : DomainEvent;
}
