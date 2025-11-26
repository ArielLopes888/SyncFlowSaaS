using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("services");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TenantId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.DurationMinutes)
                .IsRequired();

            builder.Property(x => x.Active)
                .IsRequired();

            builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique(false);
        }
    }
}
