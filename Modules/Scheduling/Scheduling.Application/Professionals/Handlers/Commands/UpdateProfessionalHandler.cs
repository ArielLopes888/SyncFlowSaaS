using Scheduling.Application.Abstractions.Handlers;
using Scheduling.Application.Professionals.Commands;
using Scheduling.Domain.Repositories;
using Scheduling.Domain.Exceptions;
using Scheduling.Domain.Entities;
using Shared.Core.Abstractions;

namespace Scheduling.Application.Professionals.Handlers.Commands;

public class UpdateProfessionalHandler
    : ICommandHandler<UpdateProfessionalCommand>
{
    private readonly IProfessionalRepository _repo;
    private readonly ITenantProvider _tenant;

    public UpdateProfessionalHandler(
        IProfessionalRepository repo,
        ITenantProvider tenant)
    {
        _repo = repo;
        _tenant = tenant;
    }

    public async Task HandleAsync(UpdateProfessionalCommand cmd, CancellationToken ct = default)
    {
        var tenantId = _tenant.GetTenantId();
        var entity = await _repo.GetByIdAsync(
                tenantId,
                cmd.ProfessionalId,
                ct
            )
            ?? throw new ProfessionalNotFoundException(cmd.ProfessionalId);

        entity.Update(cmd.Name, cmd.Specialty);

        await _repo.UpdateAsync(entity, ct);
    }
}
