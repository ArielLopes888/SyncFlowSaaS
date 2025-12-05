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
        base.OnModelCreating(modelBuilder);
    }
}
