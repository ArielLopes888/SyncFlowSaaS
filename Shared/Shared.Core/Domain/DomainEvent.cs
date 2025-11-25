using Shared.Core.Abstractions;
using System;

namespace Shared.Core.Domain;

public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
