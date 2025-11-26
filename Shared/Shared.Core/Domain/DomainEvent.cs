using Shared.Core.Abstractions;
using System;

namespace Shared.Core.Domain;

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
