using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Queries
{
    public class GetServiceByIdHandler : IQueryHandler<GetServiceByIdQuery, Service?>
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly ITenantProvider _tenantProvider;

        public GetServiceByIdHandler(
            IServiceRepository serviceRepo,
            ITenantProvider tenantProvider)
        {
            _serviceRepo = serviceRepo;
            _tenantProvider = tenantProvider;
        }

        public async Task<Service?> HandleAsync(GetServiceByIdQuery query, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            return await _serviceRepo.GetByIdAsync(query.ServiceId, tenantId, ct);
        }
    }
}