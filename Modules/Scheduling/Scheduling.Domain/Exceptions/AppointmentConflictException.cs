using Shared.Core.Exceptions;

namespace Scheduling.Domain.Exceptions
{
    public class AppointmentConflictException : DomainException
    {
        public Guid ProfessionalId { get; }
        public DateTime StartAt { get; }

        public AppointmentConflictException(Guid professionalId, DateTime startAt)
            : base($"There is a conflicting appointment for professional '{professionalId}' at {startAt:yyyy-MM-dd HH:mm}.")
        {
            ProfessionalId = professionalId;
            StartAt = startAt;
        }

        public AppointmentConflictException(Guid professionalId, DateTime startAt, string message)
            : base(message)
        {
            ProfessionalId = professionalId;
            StartAt = startAt;
        }
    }
}
