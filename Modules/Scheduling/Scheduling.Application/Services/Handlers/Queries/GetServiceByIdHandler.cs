using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;
using Shared.Core.Abstractions;
using Shared.Core.Providers;

namespace Scheduling.Application.Services.Handlers.Queries
{
    public class GetServiceByIdHandler : IQueryHandler<GetServiceByIdQuery, Service?>
    {
        private readonly ISchedulingDbContext _db;
        private readonly ITenantProvider _tenantProvider;

        public GetServiceByIdHandler(ISchedulingDbContext db, ITenantProvider tenantProvider)
        {
            _db = db;
            _tenantProvider = tenantProvider;
        }

        public Task<Service?> HandleAsync(GetServiceByIdQuery _, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            return _db.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == _.ServiceId && x.TenantId == tenantId,
                    ct
                );
        }
    }
}
