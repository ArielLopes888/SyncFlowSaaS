using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories;

public class ProfessionalRepository
    : EfRepository<Professional, SchedulingDbContext>, IProfessionalRepository
{
    public ProfessionalRepository(SchedulingDbContext db) : base(db) { }

    public async Task<Professional?> GetByIdAsync(
        Guid tenantId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Professionals
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.TenantId == tenantId &&
                p.Id == id,
                cancellationToken);
    }

    public async Task<List<Professional>> GetByTenantAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Professionals
            .AsNoTracking()
            .Where(p => p.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid tenantId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Professionals
            .AnyAsync(p =>
                p.TenantId == tenantId &&
                p.Id == id,
                cancellationToken);
    }
}
