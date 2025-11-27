using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories;

public class ProfessionalRepository
    : EfRepository<Professional, SchedulingDbContext>, IProfessionalRepository
{
    private readonly SchedulingDbContext _db;

    public ProfessionalRepository(SchedulingDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Professional?> GetByIdAsync(
        Guid tenantId,
        Guid professionalId,
        CancellationToken ct = default)
    {
        return await _db.Professionals
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.TenantId == tenantId &&
                p.Id == professionalId,
                ct);
    }
}
