using Shared.Core.Domain;
using Shared.Core.Abstractions;

namespace Scheduling.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; }
    public int DurationMinutes { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    private Service() { }

    public Service(string name, int durationMinutes, decimal price)
    {
        Name = name;
        DurationMinutes = durationMinutes;
        Price = price;
        Active = true;
    }

    public void Deactivate() => Active = false;
}
