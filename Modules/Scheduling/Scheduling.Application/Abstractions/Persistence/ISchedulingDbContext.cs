using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.Entities;

public interface ISchedulingDbContext
{
    DbSet<Appointment> Appointments { get; }
    DbSet<Professional> Professionals { get; }
    DbSet<Service> Services { get; }
    DbSet<Schedule> Schedules { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}