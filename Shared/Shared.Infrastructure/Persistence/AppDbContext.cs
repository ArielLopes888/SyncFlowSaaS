using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public AppDbContext(DbContextOptions opts, ITenantProvider tenantProvider)
        : base(opts)
    {
        _tenantProvider = tenantProvider;
    }

    // --------------------------
    // EXPOSED FOR EF FILTER
    // --------------------------
    public Guid CurrentTenantId => _tenantProvider.GetTenantId();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applies Modular EF Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        ApplyTenantGlobalFilter(modelBuilder);
        ConfigureTenantConvention(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTenantIdToNewEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    // -------------------------------------------------------
    // MULTI-TENANT IMPLEMENTATION
    // -------------------------------------------------------

    private void ApplyTenantGlobalFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(ITenantEntity.TenantId)),
                        Expression.Property(
                            Expression.Constant(this),
                            nameof(CurrentTenantId)
                        )
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
                    .Property<Guid>(nameof(ITenantEntity.TenantId))
                    .IsRequired()
                    .ValueGeneratedNever();
            }
        }
    }

    private void ApplyTenantIdToNewEntities()
    {
        var tenantId = _tenantProvider.GetTenantId();

        if (tenantId == Guid.Empty)
            throw new InvalidOperationException("TenantId não resolvido no TenantProvider.");

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is ITenantEntity)
            {
                entry.Entity.GetType()
                    .GetProperty(nameof(ITenantEntity.TenantId))
                    ?.SetValue(entry.Entity, tenantId);
            }
        }
    }
}
