using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Commands;

public record UpdateProfessionalCommand(
    Guid ProfessionalId, 
    string Name, 
    string? Specialty
);
