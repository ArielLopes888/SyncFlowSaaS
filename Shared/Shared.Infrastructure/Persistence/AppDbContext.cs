using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain;

namespace Shared.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    // DbSets concretos serão adicionados nos módulos via partial classes ou nas infra do módulo.
    // Exemplo: public DbSet<Booking> Bookings { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Dispare os eventos de domínio ANTES de salvar, se desejado (ou depois, dependendo da semântica escolhida)
        // Deixaremos a chamada de despacho de eventos para UnitOfWork para manter as responsabilidades separadas.
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configurações através da digitalização do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Filtros globais (ex: multitenancy) podem ser aplicados aqui por meio de convenções, se necessário.
    }
}

//Nota: Mapeamentos de entidades dos módulos devem ser adicionados via ApplyConfigurationsFromAssembly ou por IEntityTypeConfiguration<T> nas infra dos módulos.
//Você pode também usar partial class de AppDbContext nos módulos.