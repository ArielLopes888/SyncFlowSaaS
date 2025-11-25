using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Infrastructure.Persistence.Configurations;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public abstract void Configure(EntityTypeBuilder<T> builder);
}
