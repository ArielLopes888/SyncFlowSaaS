using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Repositories;
using Shared.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Repositories;

public class EfReadRepository<T> : IReadRepository<T> where T : class, IEntity
{
    protected readonly AppDbContext _db;

    public EfReadRepository(AppDbContext db) => _db = db;

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<T>().AnyAsync(e => e.Id == id, cancellationToken);
    }
}
