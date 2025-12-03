using Shared.Core.Abstractions;
using Shared.Core.Domain;
using System.Diagnostics;

namespace Scheduling.Domain.Entities;

public class Professional : BaseEntity, IAggregateRoot, ITenantEntity
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public string? Specialty { get; private set; }

    private Professional() { }

    public Professional(Guid tenantId, string name, string? specialty = null)
    {
        TenantId = tenantId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Specialty = specialty;
    }

    public void Update(string name, string specialty)
    {
        Name = name;
        Specialty = specialty;
    }
}
