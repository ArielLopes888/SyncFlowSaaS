using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Professionals.Commands;
using Scheduling.Domain.Entities;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Handlers.Commands
{
    public class CreateProfessionalHandler : ICommandHandler<CreateProfessionalCommand, Guid>
    {
        private readonly IProfessionalRepository _repo;
        private readonly ITenantProvider _tenantProvider;

        public CreateProfessionalHandler(
            IProfessionalRepository repo,
            ITenantProvider tenantProvider)
        {
            _repo = repo;
            _tenantProvider = tenantProvider;
        }

        public async Task<Guid> HandleAsync(CreateProfessionalCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var professional = new Professional(
                tenantId,
                cmd.Name,
                cmd.Specialty
            );

            await _repo.AddAsync(professional, ct);

            return professional.Id;
        }
    }
}
