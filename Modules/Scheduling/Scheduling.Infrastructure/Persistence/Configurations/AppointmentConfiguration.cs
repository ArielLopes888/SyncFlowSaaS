using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.ProfessionalId).IsRequired();
        builder.Property(a => a.ServiceId).IsRequired();

        builder.Property(a => a.StartAt)
               .IsRequired()
               .HasColumnType("timestamp without time zone");

        builder.Property(a => a.EndAt)
               .IsRequired()
               .HasColumnType("timestamp without time zone");

        builder.Property(a => a.ClientName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(a => a.Phone)
               .HasMaxLength(20);

        builder.Property(a => a.Status)
               .HasConversion<int>()
               .IsRequired();
        builder.Property(a => a.TenantId)
                .IsRequired();

        builder.HasIndex(a => new { a.TenantId, a.ProfessionalId });

    }
}
