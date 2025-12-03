using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.Entities;
using Shared.Infrastructure.Persistence;
using Shared.Core.Abstractions;

namespace Scheduling.Infrastructure.Persistence;

public class SchedulingDbContext : AppDbContext
{
    public SchedulingDbContext(
        DbContextOptions<SchedulingDbContext> options,
        ITenantProvider tenantProvider)
        : base(options, tenantProvider)
    {
    }

    public DbSet<Service> Services => Set<Service>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<ScheduleOverride> ScheduleOverrides => Set<ScheduleOverride>();
    public DbSet<TimeOff> TimeOffs => Set<TimeOff>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Base aplica:
        // - ApplyConfigurationsFromAssembly
        // - global tenant filter
        // - tenant conventions
        base.OnModelCreating(modelBuilder);

        // SERVICE
        modelBuilder.Entity<Service>(cfg =>
        {
            cfg.ToTable("Services");
            cfg.HasKey(x => x.Id);

            cfg.Property(x => x.Name).IsRequired().HasMaxLength(150);
            cfg.Property(x => x.Price).HasColumnType("decimal(10,2)");
            cfg.Property(x => x.DurationMinutes).IsRequired();
        });

        // PROFESSIONAL
        modelBuilder.Entity<Professional>(cfg =>
        {
            cfg.ToTable("Professionals");
            cfg.HasKey(x => x.Id);

            cfg.Property(x => x.Name).IsRequired().HasMaxLength(150);
            cfg.Property(x => x.Specialty).HasMaxLength(150);
        });

        // SCHEDULE
        modelBuilder.Entity<Schedule>(cfg =>
        {
            cfg.ToTable("Schedules");
            cfg.HasKey(x => x.Id);
            cfg.Property(x => x.TenantId).IsRequired();
            cfg.Property(x => x.Day).IsRequired();
            cfg.Property(x => x.Start).IsRequired();
            cfg.Property(x => x.End).IsRequired();
            cfg.Property(x => x.IntervalMinutes).IsRequired();

            cfg.HasOne<Professional>()
               .WithMany()
               .HasForeignKey(x => x.ProfessionalId);

            cfg.HasMany(x => x.Overrides)
               .WithOne()
               .HasForeignKey(o => o.ScheduleId)
               .OnDelete(DeleteBehavior.Cascade);

            cfg.HasMany(x => x.TimeOffs)
                .WithOne()
                .HasForeignKey(t => t.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // APPOINTMENT
        modelBuilder.Entity<Appointment>(cfg =>
        {
            cfg.ToTable("Appointments");
            cfg.HasKey(x => x.Id);

            cfg.Property(x => x.ClientName).HasMaxLength(150);
            cfg.Property(x => x.Phone).HasMaxLength(50);

            cfg.HasOne<Professional>()
                .WithMany()
                .HasForeignKey(x => x.ProfessionalId);

            cfg.HasOne<Service>()
                .WithMany()
                .HasForeignKey(x => x.ServiceId);
        });

    }
}
