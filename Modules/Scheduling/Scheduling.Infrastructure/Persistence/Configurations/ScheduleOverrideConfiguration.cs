using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence.Configurations;

public class ScheduleOverrideConfiguration : IEntityTypeConfiguration<ScheduleOverride>
{
    public void Configure(EntityTypeBuilder<ScheduleOverride> builder)
    {
        builder.ToTable("ScheduleOverrides");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ScheduleId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Start)
            .HasColumnType("time")        
            .IsRequired(false);

        builder.Property(x => x.End)
            .HasColumnType("time")
            .IsRequired(false);

        builder.Property(x => x.IsClosed)
            .IsRequired();

     
        builder.HasOne<Schedule>()
            .WithMany(s => s.Overrides)
            .HasForeignKey(x => x.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
