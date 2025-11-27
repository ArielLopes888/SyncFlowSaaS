using Shared.Core.Exceptions;

namespace Scheduling.Domain.Exceptions
{
    public class ProfessionalNotFoundException : DomainException
    {
        public Guid ProfessionalId { get; }

        public ProfessionalNotFoundException(Guid professionalId)
            : base($"Professional with ID '{professionalId}' was not found.")
        {
            ProfessionalId = professionalId;
        }

        public ProfessionalNotFoundException(Guid professionalId, string message)
            : base(message)
        {
            ProfessionalId = professionalId;
        }
    }
}
