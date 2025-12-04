using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories;

public class ScheduleRepository : EfRepository<Schedule, SchedulingDbContext>, IScheduleRepository
{
    public ScheduleRepository(SchedulingDbContext db) : base(db) { }

    public async Task<Schedule?> GetByIdWithDetailsAsync(Guid tenantId, Guid id, CancellationToken ct = default)
    {
        return await _db.Schedules
            .AsNoTracking()
            .Include(s => s.Overrides)
            .Include(s => s.TimeOffs)
            .FirstOrDefaultAsync(s => s.TenantId == tenantId && s.Id == id, ct);
    }

    public async Task<List<Schedule>> GetByProfessionalAndDayOfWeekAsync(Guid tenantId, Guid professionalId, DayOfWeek day, CancellationToken ct = default)
    {
        return await _db.Schedules
            .AsNoTracking()
            .Include(s => s.Overrides)
            .Include(s => s.TimeOffs)
            .Where(s => s.TenantId == tenantId && s.ProfessionalId == professionalId && s.Day == day)
            .ToListAsync(ct);
    }

    public async Task<List<Schedule>> GetByProfessionalAsync(Guid tenantId, Guid professionalId, CancellationToken ct = default)
    {
        return await _db.Schedules
            .AsNoTracking()
            .Include(s => s.Overrides)
            .Include(s => s.TimeOffs)
            .Where(s => s.TenantId == tenantId && s.ProfessionalId == professionalId)
            .ToListAsync(ct);
    }

    public async Task<List<Schedule>> GetByTenantAsync(
    Guid tenantId,
    CancellationToken cancellationToken = default)
    {
        return await _db.Schedules
            .AsNoTracking()
            .Where(a => a.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
