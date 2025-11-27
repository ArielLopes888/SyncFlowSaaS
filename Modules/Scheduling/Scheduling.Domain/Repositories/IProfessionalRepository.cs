using Shared.Core.Repositories;
using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

public interface IProfessionalRepository : IRepository<Professional>
{
    Task<Professional?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default);
}
