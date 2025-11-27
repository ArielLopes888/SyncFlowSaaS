using Shared.Core.Exceptions;

namespace Scheduling.Domain.Exceptions
{
    public class ServiceNotFoundException : DomainException
    {
        public Guid ServiceId { get; }

        public ServiceNotFoundException(Guid serviceId)
            : base($"Service with ID '{serviceId}' was not found.")
        {
            ServiceId = serviceId;
        }

        public ServiceNotFoundException(Guid serviceId, string message)
            : base(message)
        {
            ServiceId = serviceId;
        }
    }
}
