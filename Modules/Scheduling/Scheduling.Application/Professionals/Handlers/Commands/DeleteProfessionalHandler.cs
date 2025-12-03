using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Professionals.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Scheduling.Domain.Entities;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Handlers.Commands
{
    public class DeleteProfessionalHandler : ICommandHandler<DeleteProfessionalCommand>
    {
        private readonly IProfessionalRepository _repo;
        private readonly ITenantProvider _tenantProvider;

        public DeleteProfessionalHandler(
            IProfessionalRepository repo,
            ITenantProvider tenantProvider)
        {
            _repo = repo;
            _tenantProvider = tenantProvider;
        }

        public async Task HandleAsync(DeleteProfessionalCommand cmd, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var entity = await _repo.GetByIdAsync(tenantId, cmd.ProfessionalId, ct)
                ?? throw new ProfessionalNotFoundException(cmd.ProfessionalId);

            await _repo.DeleteAsync(entity, ct);
        }
    }
}
