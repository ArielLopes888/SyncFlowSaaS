using Shared.Core.Abstractions;

namespace Shared.Infrastructure.Events
{
    public interface IHandle<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
    }

}
