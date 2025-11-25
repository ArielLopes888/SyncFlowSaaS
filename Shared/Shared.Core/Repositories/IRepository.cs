using Shared.Core.Abstractions;

namespace Shared.Core.Repositories;

public interface IRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
