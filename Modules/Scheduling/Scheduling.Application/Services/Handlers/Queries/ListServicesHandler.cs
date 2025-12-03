using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Services.Queries;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Services.Handlers.Queries
{
    public class ListServicesHandler : IQueryHandler<ListServicesQuery, List<Service>>
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly ITenantProvider _tenantProvider;

        public ListServicesHandler(
            IServiceRepository serviceRepo,
            ITenantProvider tenantProvider)
        {
            _serviceRepo = serviceRepo;
            _tenantProvider = tenantProvider;
        }

        public async Task<List<Service>> HandleAsync(ListServicesQuery query, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            return await _serviceRepo.GetByTenantAsync(tenantId, ct);
        }
    }
}