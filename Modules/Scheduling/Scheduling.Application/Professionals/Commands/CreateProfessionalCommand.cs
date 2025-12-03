using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Commands;

public record CreateProfessionalCommand(
    string Name,
    string? Specialty
);

