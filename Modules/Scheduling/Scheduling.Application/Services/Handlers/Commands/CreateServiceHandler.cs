using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Services.Commands;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories; 
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Commands
{
    public class CreateServiceHandler : ICommandHandler<CreateServiceCommand, Guid>
    {
        private readonly IServiceRepository _serviceRepo; 
        private readonly ITenantProvider _tenantProvider;

        public CreateServiceHandler(
            IServiceRepository serviceRepo, 
            ITenantProvider tenantProvider)
        {
            _serviceRepo = serviceRepo;
            _tenantProvider = tenantProvider;
        }

        public async Task<Guid> HandleAsync(CreateServiceCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var service = new Service(tenantId, cmd.Name, cmd.Price, cmd.DurationMinutes);

            await _serviceRepo.AddAsync(service, ct); 
            return service.Id;
        }
    }
}