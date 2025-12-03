using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Services.Commands;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Commands
{
    public class UpdateServiceHandler : ICommandHandler<UpdateServiceCommand>
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly ITenantProvider _tenantProvider;

        public UpdateServiceHandler(
            IServiceRepository serviceRepo,
            ITenantProvider tenantProvider)
        {
            _serviceRepo = serviceRepo;
            _tenantProvider = tenantProvider;
        }

        public async Task HandleAsync(UpdateServiceCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var service = await _serviceRepo.GetByIdAsync(cmd.ServiceId, tenantId, ct);

            if (service is null)
                throw new KeyNotFoundException("Service not found.");

            service.Update(cmd.Name, cmd.Price, cmd.DurationMinutes);

            await _serviceRepo.UpdateAsync(service, ct);
        }
    }
}