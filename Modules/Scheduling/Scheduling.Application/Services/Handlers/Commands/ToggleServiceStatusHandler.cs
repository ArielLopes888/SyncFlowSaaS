using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Commands;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Commands
{
    public class ToggleServiceStatusHandler : ICommandHandler<ToggleServiceStatusCommand>
    {
        private readonly ISchedulingDbContext _db;
        private readonly ITenantProvider _tenantProvider;

        public ToggleServiceStatusHandler(
            ISchedulingDbContext db,
            ITenantProvider tenant
            )
        {
            _db = db;
            _tenantProvider = tenant;
        }

        public async Task HandleAsync(ToggleServiceStatusCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var service = await _db.Services
                .FirstOrDefaultAsync(x => x.Id == cmd.ServiceId && x.TenantId == tenantId, ct);

            if (service is null)
                throw new KeyNotFoundException("Service not found.");

            if (cmd.Active)
                service.Activate();
            else
                service.Deactivate();

            await _db.SaveChangesAsync(ct);
        }
    }

}
