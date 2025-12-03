// IScheduleRepository.cs
using Shared.Core.Repositories;
using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<List<Schedule>> GetByProfessionalAndDayOfWeekAsync(
        Guid tenantId, Guid professionalId, DayOfWeek day, CancellationToken ct = default);

    Task<List<Schedule>> GetByProfessionalAsync(
        Guid tenantId, Guid professionalId, DateTime fromUtc, DateTime toUtc, CancellationToken ct = default);
}
