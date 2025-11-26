using Shared.Core.Abstractions;
using Shared.Core.Domain;

namespace Scheduling.Domain.Entities;

public class Service : BaseEntity, IAggregateRoot
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public int DurationMinutes { get; private set; }
    public bool Active { get; private set; }

    private Service() { }

    public Service(Guid tenantId, string name, int durationMinutes)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Service name is required.", nameof(name));

        if (durationMinutes <= 0)
            throw new ArgumentException("Duration must be greater than zero.", nameof(durationMinutes));

        TenantId = tenantId;
        Name = name;
        DurationMinutes = durationMinutes;
        Active = true;
    }

    public void Deactivate() => Active = false;
    public void Activate() => Active = true;
}
