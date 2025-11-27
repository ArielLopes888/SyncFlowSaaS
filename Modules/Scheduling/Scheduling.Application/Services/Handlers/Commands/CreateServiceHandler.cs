using Scheduling.Application.Abstractions.Persistence;
using Scheduling.Application.Services.Commands;
using Scheduling.Domain.Entities;
using Scheduling.Application.Abstractions.Handlers;
using Shared.Core.Abstractions;
namespace Scheduling.Application.Services.Handlers.Commands
{
    public class CreateServiceHandler : ICommandHandler<CreateServiceCommand, Guid>
    {
        private readonly ISchedulingDbContext _db;
        private readonly ITenantProvider _tenantProvider;

        public CreateServiceHandler(
            ISchedulingDbContext db,
            ITenantProvider tenant
            )
        {
            _db = db;
            _tenantProvider = tenant;
        }

        public async Task<Guid> HandleAsync(CreateServiceCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var service = new Service(tenantId, cmd.Name, cmd.Price, cmd.DurationMinutes);

            _db.Services.Add(service);
            await _db.SaveChangesAsync(ct);

            return service.Id;
        }
    }

}
