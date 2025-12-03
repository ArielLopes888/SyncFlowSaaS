using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;
using Polly;

namespace Scheduling.Infrastructure.Repositories;

public class ServiceRepository
    : EfRepository<Service, SchedulingDbContext>, IServiceRepository
{

    public ServiceRepository(SchedulingDbContext db) : base(db){}

    public async Task<Service?> GetByIdAsync(
        Guid tenantId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Services
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            s => s.Id == id && s.TenantId == tenantId,
                            cancellationToken);
    }

    public async Task<List<Service>> GetByTenantAsync(
        Guid tenantId, 
        CancellationToken cancellationToken = default)
    {
        return await _db.Services
            .AsNoTracking()
            .Where(s => s.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
