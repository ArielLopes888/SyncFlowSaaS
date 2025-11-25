using Shared.Core.Repositories;

namespace Shared.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private readonly IDomainEventDispatcher _dispatcher; 

    public EfUnitOfWork(AppDbContext db, IDomainEventDispatcher dispatcher)
    {
        _db = db;
        _dispatcher = dispatcher;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Opcional: coletar eventos de domínio e enviá-los APÓS o salvamento bem-sucedido
        var result = await _db.SaveChangesAsync(cancellationToken);

        // Coletar e despachar eventos de domínio
        await _dispatcher.DispatchDomainEventsAsync(_db, cancellationToken);

        return result;
    }
}
