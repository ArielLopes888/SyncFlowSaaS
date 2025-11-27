using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Appointments.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;

namespace Scheduling.Application.Appointments.Handlers.Queries;

public class ListByCustomerHandler : IQueryHandler<ListByCustomerQuery, List<Appointment>>
{
    private readonly IAppointmentRepository _repo;

    public ListByCustomerHandler(IAppointmentRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Appointment>> HandleAsync(ListByCustomerQuery query, CancellationToken ct = default)
    {
        return _repo.GetByCustomerAsync(
            query.TenantId,
            query.ClientName,
            ct
        );
    }
}
