using Shared.Core.Abstractions;
using System.Windows.Input;

namespace Scheduling.Application.Professionals.Commands;

public record DeleteProfessionalCommand(
    Guid ProfessionalId
);
