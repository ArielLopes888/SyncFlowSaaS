using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain;
using System.Linq.Expressions;
using Shared.Core.Abstractions;

namespace Shared.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public AppDbContext(
        DbContextOptions<AppDbContext> opts,
        ITenantProvider tenantProvider) : base(opts)
    {
        _tenantProvider = tenantProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica mapeamentos modularizados
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Aplica filtro Multi-Tenant global
        ApplyTenantGlobalFilter(modelBuilder);

        // Configura automaticamente o TenantId em todas as entidades do domínio que precisarem
        ConfigureTenantConvention(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTenantIdToNewEntities();

        return await base.SaveChangesAsync(cancellationToken);
    }

    // -------------------------------------------------------------------------
    //  MULTI-TENANT IMPLEMENTAÇÃO
    // -------------------------------------------------------------------------

    private void ApplyTenantGlobalFilter(ModelBuilder modelBuilder)
    {
        var tenantId = _tenantProvider.GetTenantId();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Aplica filtro apenas em entidades que possuem TenantId
            if (entityType.ClrType.IsAssignableTo(typeof(ITenantEntity)))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(ITenantEntity.TenantId)),
                        Expression.Constant(tenantId)
                    ),
                    parameter
                );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }

    private void ConfigureTenantConvention(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property<string>(nameof(ITenantEntity.TenantId))
                    .IsRequired();
            }
        }
    }

    private void ApplyTenantIdToNewEntities()
    {
        var tenantId = _tenantProvider.GetTenantId();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added &&
                entry.Entity is ITenantEntity tenantEntity)
            {
                tenantEntity.TenantId = tenantId;
            }
        }
    }
}
