using Shared.Core.Repositories;
using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<Appointment?> GetByIdWithDetailsAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default);

    Task<List<Appointment>> GetByProfessionalAndRangeAsync(Guid tenantId, Guid professionalId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    Task<bool> ExistsConflictingAsync(Guid tenantId, Guid professionalId, DateTime start, DateTime end, CancellationToken cancellationToken = default);

    Task<List<Appointment>> GetByCustomerAsync(Guid tenantId, string clientName, CancellationToken cancellationToken = default);
}
