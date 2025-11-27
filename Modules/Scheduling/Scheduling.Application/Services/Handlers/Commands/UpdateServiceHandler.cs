using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Commands;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Commands
{
    public class UpdateServiceHandler : ICommandHandler<UpdateServiceCommand>
    {
        private readonly ISchedulingDbContext _db;
        private readonly ITenantProvider _tenantProvider;

        public UpdateServiceHandler(
            ISchedulingDbContext db,
            ITenantProvider tenant)
        {
            _db = db;
            _tenantProvider = tenant;
        }

        public async Task HandleAsync(UpdateServiceCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var service = await _db.Services
                .FirstOrDefaultAsync(x => x.Id == cmd.ServiceId && x.TenantId == tenantId, ct);

            if (service is null)
                throw new KeyNotFoundException("Service not found.");

            service.Update(cmd.Name,cmd.Price, cmd.DurationMinutes);

            await _db.SaveChangesAsync(ct);
        }
    }

}
