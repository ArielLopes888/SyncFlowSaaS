using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.Abstractions.Persistence
{
    public interface ISchedulingDbContext
    {
        DbSet<Service> Services { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
