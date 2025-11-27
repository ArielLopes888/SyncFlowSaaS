using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories;

public class AppointmentRepository
    : EfRepository<Appointment, SchedulingDbContext>, IAppointmentRepository
{
    private readonly SchedulingDbContext _db;

    public AppointmentRepository(SchedulingDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Appointment?> GetByIdWithDetailsAsync(
        Guid tenantId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a =>
                a.TenantId == tenantId &&
                a.Id == id,
                cancellationToken);
    }

    public async Task<List<Appointment>> GetByProfessionalAndRangeAsync(
        Guid tenantId,
        Guid professionalId,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken = default)
    {
        return await _db.Appointments
            .AsNoTracking()
            .Where(a =>
                a.TenantId == tenantId &&
                a.ProfessionalId == professionalId &&
                a.StartAt >= from &&
                a.EndAt <= to)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsConflictingAsync(
        Guid tenantId,
        Guid professionalId,
        DateTime start,
        DateTime end,
        CancellationToken cancellationToken = default)
    {
        return await _db.Appointments
            .AnyAsync(a =>
                a.TenantId == tenantId &&
                a.ProfessionalId == professionalId &&
                (start < a.EndAt && end > a.StartAt),
                cancellationToken);
    }

    public async Task<List<Appointment>> GetByCustomerAsync(
        Guid tenantId,
        string clientName,
        CancellationToken cancellationToken = default)
    {
        return await _db.Appointments
            .AsNoTracking()
            .Where(a =>
                a.TenantId == tenantId &&
                EF.Functions.Collate(a.ClientName, "SQL_Latin1_General_CP1_CI_AS") == clientName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Appointment>> GetByTenantAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Appointments
            .AsNoTracking()
            .Where(a => a.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
