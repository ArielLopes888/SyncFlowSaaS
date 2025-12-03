using Shared.Core.Repositories;
using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

public interface IServiceRepository : IRepository<Service>
{
    Task<Service?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default);
    Task<List<Service>> GetByTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);
}