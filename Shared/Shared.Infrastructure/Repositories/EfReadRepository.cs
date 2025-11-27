using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Repositories;
using Shared.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Repositories;

public class EfReadRepository<T, TContext> : IReadRepository<T>
    where T : class, IEntity
    where TContext : DbContext
{
    protected readonly TContext _db;

    public EfReadRepository(TContext db)
    {
        _db = db;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.Set<T>().FindAsync(new object[] { id }, cancellationToken);

    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        => await _db.Set<T>().ToListAsync(cancellationToken);

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.Set<T>().AnyAsync(e => e.Id == id, cancellationToken);
}
