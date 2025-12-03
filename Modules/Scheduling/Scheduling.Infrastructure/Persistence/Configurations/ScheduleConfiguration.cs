using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.Property(x => x.ProfessionalId)
            .IsRequired();

        builder.Property(x => x.Day)
            .IsRequired();

        builder.Property(x => x.Start)
            .IsRequired();

        builder.Property(x => x.End)
            .IsRequired();

        builder.Property(x => x.IntervalMinutes)
            .IsRequired();

        // CHILD COLLECTIONS
        builder.HasMany(x => x.Overrides)
            .WithOne()
            .HasForeignKey(o => o.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TimeOffs)
            .WithOne()
            .HasForeignKey(t => t.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
