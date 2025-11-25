using Shared.Core.Domain;
using Shared.Core.Abstractions;

namespace Scheduling.Domain.Entities;

public class Professional : BaseEntity, IAggregateRoot
{
    private readonly List<Service> _services = new();
    private readonly List<Schedule> _schedules = new();

    public string Name { get; private set; }
    public string? Specialty { get; private set; }

    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();
    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();

    private Professional() { } // EF Core

    public Professional(string name, string? specialty = null)
    {
        Name = name;
        Specialty = specialty;
    }

    public void AddService(Service service)
    {
        _services.Add(service);
    }

    public void AddSchedule(Schedule schedule)
    {
        _schedules.Add(schedule);
    }
}
