using Shared.Core.Abstractions;
using Shared.Core.Domain;

namespace Scheduling.Domain.Entities;

public class Service : BaseEntity, IAggregateRoot, ITenantEntity
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int DurationMinutes { get; private set; }
    public bool Active { get; private set; }

    private Service() { }

    public Service(Guid tenantId, string name, decimal price, int durationMinutes)
    {
        TenantId = tenantId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        DurationMinutes = durationMinutes;
        Active = true;
    }

    public void Update(string name, decimal price, int durationMinutes)
    {
        Name = name;
        Price = price;
        DurationMinutes = durationMinutes;
    }

    public void Activate() => Active = true;

    public void Deactivate() => Active = false;
}
