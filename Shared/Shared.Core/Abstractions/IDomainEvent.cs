namespace Shared.Core.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
