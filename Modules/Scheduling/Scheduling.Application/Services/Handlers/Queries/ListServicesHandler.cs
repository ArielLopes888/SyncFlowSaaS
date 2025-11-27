using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;
using Shared.Core.Abstractions;
using Shared.Core.Providers;

namespace Scheduling.Application.Services.Handlers.Queries
{
    public class ListServicesHandler : IQueryHandler<ListServicesQuery, List<Service>>
    {
        private readonly ISchedulingDbContext _db;
        private readonly ITenantProvider _tenantProvider;

        public ListServicesHandler(
            ISchedulingDbContext db,
            ITenantProvider tenantProvider)
        {
            _db = db;
            _tenantProvider = tenantProvider;
        }

        public async Task<List<Service>> HandleAsync(ListServicesQuery query, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            return await _db.Services
                .AsNoTracking()
                .Where(s => s.TenantId == tenantId)
                .ToListAsync(ct);
        }
    }
}
