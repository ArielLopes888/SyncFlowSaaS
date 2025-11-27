using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Persistence;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories;

public class ServiceRepository : EfRepository<Service>, IServiceRepository
{
    private readonly SchedulingDbContext _db;
    public ServiceRepository(SchedulingDbContext db) : base(db) => _db = db;


    public async Task<Service?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Service>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == id && s.TenantId == tenantId, cancellationToken);
    }
    public async Task<Service?> GetByIdWithServicesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Service>()
                        .Include(p => p.Active) // configure navigation
                        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}

