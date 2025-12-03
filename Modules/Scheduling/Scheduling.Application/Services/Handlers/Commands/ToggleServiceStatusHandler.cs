using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Services.Commands;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Commands
{
    public class ToggleServiceStatusHandler : ICommandHandler<ToggleServiceStatusCommand>
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly ITenantProvider _tenantProvider;

        public ToggleServiceStatusHandler(
            IServiceRepository serviceRepo,
            ITenantProvider tenantProvider)
        {
            _serviceRepo = serviceRepo;
            _tenantProvider = tenantProvider;
        }

        public async Task HandleAsync(ToggleServiceStatusCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var service = await _serviceRepo.GetByIdAsync(cmd.ServiceId, tenantId, ct);

            if (service is null)
                throw new KeyNotFoundException("Service not found.");

            if (cmd.Active)
                service.Activate();
            else
                service.Deactivate();

            await _serviceRepo.UpdateAsync(service, ct);
        }
    }
}