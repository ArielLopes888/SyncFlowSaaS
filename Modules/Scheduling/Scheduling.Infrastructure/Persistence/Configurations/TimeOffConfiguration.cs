using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence.Configurations;

public class TimeOffConfiguration : IEntityTypeConfiguration<TimeOff>
{
    public void Configure(EntityTypeBuilder<TimeOff> builder)
    {
        builder.ToTable("TimeOffs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ScheduleId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.EndDate)
            .HasColumnType("datetime2")
            .IsRequired();


        builder.HasOne<Schedule>()
            .WithMany(s => s.TimeOffs)
            .HasForeignKey(x => x.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
