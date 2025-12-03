// ScheduleRepository.cs
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories;

public class ScheduleRepository : EfRepository<Schedule, SchedulingDbContext>, IScheduleRepository
{
    public ScheduleRepository(SchedulingDbContext db) : base(db) { }

    public async Task<List<Schedule>> GetByProfessionalAndDayOfWeekAsync(
        Guid tenantId, Guid professionalId, DayOfWeek day, CancellationToken ct = default)
    {
        return await _db.Schedules
            .AsNoTracking()
            .Where(s => s.TenantId == tenantId && s.ProfessionalId == professionalId && s.Day == day)
            .ToListAsync(ct);
    }

    public async Task<List<Schedule>> GetByProfessionalAsync(
        Guid tenantId, Guid professionalId, DateTime fromUtc, DateTime toUtc, CancellationToken ct = default)
    {
        // Convert range of dates into set of DayOfWeek values and query schedules — simpler approach:
        // get all schedules for professional (they are recurring), caller will filter by date/day logic.
        return await _db.Schedules
            .AsNoTracking()
            .Where(s => s.TenantId == tenantId && s.ProfessionalId == professionalId)
            .ToListAsync(ct);
    }
}
