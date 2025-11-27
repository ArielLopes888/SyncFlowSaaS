using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Services.Handlers.Queries
{
    public class GetServiceByIdHandler : IQueryHandler<GetServiceByIdQuery, Service?>
    {
        private readonly ISchedulingDbContext _db;

        public GetServiceByIdHandler(ISchedulingDbContext db)
        {
            _db = db;
        }

        public Task<Service?> HandleAsync(GetServiceByIdQuery query, CancellationToken ct = default)
        {
            return _db.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == query.ServiceId && x.TenantId == query.TenantId, ct);
        }
    }

}
