using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Services.Handlers.Queries
{
    public class ListServicesHandler : IQueryHandler<ListServicesQuery, List<Service>>
    {
        private readonly ISchedulingDbContext _db;

        public ListServicesHandler(ISchedulingDbContext db)
        {
            _db = db;
        }

        public async Task<List<Service>> HandleAsync(ListServicesQuery query, CancellationToken ct = default)
        {
            return await _db.Services
                .AsNoTracking()
                .Where(x => x.TenantId == query.TenantId)
                .ToListAsync(ct);
        }
    }

}
