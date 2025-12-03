using Shared.Core.Abstractions;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Professionals.Queries;

public record GetProfessionalByIdQuery(
    Guid ProfessionalId
);
