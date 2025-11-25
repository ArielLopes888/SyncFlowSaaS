using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions;
using Shared.Core.Exceptions;
using Shared.Infrastructure.Persistence;

namespace Shared.Infrastructure.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchDomainEventsAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        // Find entries with domain events
        var entitiesWithEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Clear domain events to avoid re-dispatching
        entitiesWithEvents.ForEach(e => e.ClearDomainEvents());

        // For each event, resolve a handler (convention-based: IHandle<TEvent>)
        foreach (var domainEvent in domainEvents)
        {
            var eventType = domainEvent.GetType();
           /* var handlerType = typeof(INotificationHandler<>).AssemblyQualifiedName;*/ // if using MediatR; else use custom

            // We'll use a simple convention: handlers implement IHandle<TEvent>
            var handlerInterfaceType = typeof(IHandle<>).MakeGenericType(eventType);
            var handlers = _serviceProvider.GetServices(handlerInterfaceType);

            foreach (var handler in handlers)
            {
                try
                {
                    // invoke Handle method
                    var handleMethod = handlerInterfaceType.GetMethod("Handle");
                    var task = (Task)handleMethod!.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
                    await task;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error dispatching domain event {Event}", domainEvent.GetType().Name);
                    // Decide: swallow or rethrow. For now, swallow to not break UoW; but log
                }
            }
        }
    }
}
