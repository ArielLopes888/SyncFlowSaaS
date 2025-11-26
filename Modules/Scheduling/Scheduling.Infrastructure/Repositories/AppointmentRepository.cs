using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Persistence;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Scheduling.Infrastructure.Repositories;

public class AppointmentRepository : EfRepository<Appointment>, IAppointmentRepository
{
    private readonly AppDbContext _db;

    public AppointmentRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Appointment?> GetByIdWithDetailsAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Appointment>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.TenantId == tenantId && a.Id == id, cancellationToken);
    }

    public async Task<List<Appointment>> GetByProfessionalAndRangeAsync(Guid tenantId, Guid professionalId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Appointment>()
                        .AsNoTracking()
                        .Where(a =>
                            a.TenantId == tenantId &&
                            a.ProfessionalId == professionalId &&
                            a.StartAt >= from &&
                            a.EndAt <= to)
                        .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsConflictingAsync(Guid tenantId, Guid professionalId, DateTime start, DateTime end, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Appointment>()
            .AnyAsync(a =>
                a.TenantId == tenantId &&
                a.ProfessionalId == professionalId &&
                (start < a.EndAt && end > a.StartAt),
                cancellationToken);
    }

    public async Task<List<Appointment>> GetByCustomerAsync(Guid tenantId, string clientName, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Appointment>()
                        .AsNoTracking()
                        .Where(a =>
                            a.TenantId == tenantId &&
                            a.ClientName.ToLower() == clientName.ToLower())
                        .ToListAsync(cancellationToken);
    }

}
