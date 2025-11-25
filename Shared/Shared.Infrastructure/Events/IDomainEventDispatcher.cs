using Shared.Core.Abstractions;
using Shared.Infrastructure.Persistence;

namespace Shared.Infrastructure.Events;

public interface IDomainEventDispatcher
{
    Task DispatchDomainEventsAsync(AppDbContext dbContext, CancellationToken cancellationToken = default);
}
