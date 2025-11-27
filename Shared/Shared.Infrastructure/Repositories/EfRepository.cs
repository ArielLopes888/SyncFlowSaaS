using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Repositories;
using Shared.Infrastructure.Persistence;

namespace Shared.Infrastructure.Repositories;

public class EfRepository<T, TContext>
    : EfReadRepository<T, TContext>, IRepository<T>
    where T : class, IAggregateRoot
    where TContext : DbContext
{
    public EfRepository(TContext db) : base(db) { }

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _db.Set<T>().AddAsync(entity, cancellationToken);

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _db.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _db.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}
