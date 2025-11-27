using Shared.Core.Exceptions;

namespace Scheduling.Domain.Exceptions;

public class AppointmentNotFoundException : DomainException
{
    public Guid AppointmentId { get; }

    public AppointmentNotFoundException(Guid appointmentId)
        : base($"Appointment with ID '{appointmentId}' was not found.")
    {
        AppointmentId = appointmentId;
    }

    public AppointmentNotFoundException(Guid appointmentId, string message)
        : base(message)
    {
        AppointmentId = appointmentId;
    }
}