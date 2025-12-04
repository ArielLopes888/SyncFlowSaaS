using Shared.Core.Repositories;
using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<Schedule?> GetByIdWithDetailsAsync(Guid tenantId, Guid id, CancellationToken ct = default);
    Task<List<Schedule>> GetByProfessionalAndDayOfWeekAsync(Guid tenantId, Guid professionalId, DayOfWeek day, CancellationToken ct = default);

    Task<List<Schedule>> GetByProfessionalAsync(Guid tenantId, Guid professionalId, CancellationToken ct = default);

    Task<List<Schedule>> GetByTenantAsync(
    Guid tenantId,
    CancellationToken cancellationToken = default);
}
