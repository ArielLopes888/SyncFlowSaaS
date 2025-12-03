using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Appointments;
using Scheduling.Application.Availability.Responses;
using Scheduling.Domain.Repositories;
using Shared.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Services.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly ISchedulingDbContext _db;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly ITenantProvider _tenantProvider;

        public AvailabilityService(
            ISchedulingDbContext db,
            IAppointmentRepository appointmentRepo,
            IServiceRepository serviceRepo,
            ITenantProvider tenantProvider)
        {
            _db = db;
            _appointmentRepo = appointmentRepo;
            _serviceRepo = serviceRepo;
            _tenantProvider = tenantProvider;
        }

        public async Task<List<TimeSlot>> GetDailyAvailabilityAsync(Guid professionalId, DateOnly date, Guid serviceId, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            // 1) get service to determine duration
            var service = await _serviceRepo.GetByIdAsync(tenantId, serviceId, ct);
            if (service is null)
                throw new KeyNotFoundException("Service not found.");

            var duration = TimeSpan.FromMinutes(service.DurationMinutes);

            // 2) determine tenant timezone
            // TODO: Replace with real Tenant.TimeZone lookup if you have a tenant repository.
            // For now we fallback to UTC.
            TimeZoneInfo tenantTz = TimeZoneInfo.Utc;

            // If you have a Tenant repository, inject it and set tenantTz = TimeZoneInfo.FindSystemTimeZoneById(tenant.TimeZoneId)
            // e.g. var tenant = await _tenantRepo.GetByIdAsync(tenantId); tenantTz = TimeZoneInfo.FindSystemTimeZoneById(tenant.TimeZoneId);

            // 3) compute day bounds in tenant local and convert to UTC
            var localDayStart = date.ToDateTime(TimeOnly.MinValue); // midnight local
            var localDayEnd = date.ToDateTime(TimeOnly.MaxValue); // end of day local

            var dayStartUtc = TimeZoneInfo.ConvertTimeToUtc(localDayStart, tenantTz);
            var dayEndUtc = TimeZoneInfo.ConvertTimeToUtc(localDayEnd, tenantTz);

            // 4) load schedules for that day-of-week (include overrides/timeoffs)
            var dayOfWeek = date.DayOfWeek;
            var schedules = await _db.Schedules
                .AsNoTracking()
                .Where(s => s.TenantId == tenantId && s.ProfessionalId == professionalId && s.Day == dayOfWeek)
                .Include(s => s.Overrides)
                .Include(s => s.TimeOffs)
                .ToListAsync(ct);

            if (!schedules.Any())
                return new List<TimeSlot>();

            // 5) load appointments overlapping the day (UTC)
            var appointments = await _appointmentRepo.GetByProfessionalAndRangeAsync(tenantId, professionalId, dayStartUtc, dayEndUtc, ct);

            var occupiedIntervals = new List<(DateTime StartUtc, DateTime EndUtc)>();

            // appointments -> to UTC intervals
            occupiedIntervals.AddRange(appointments.Select(a => (a.StartAt.ToUniversalTime(), a.EndAt.ToUniversalTime())));

            // 6) add schedule TimeOffs (they are stored as StartDate/EndDate - assume DateTime with Kind unspecified)
            foreach (var s in schedules)
            {
                foreach (var to in s.TimeOffs)
                {
                    var sUtc = to.StartDate.ToUniversalTime();
                    var eUtc = to.EndDate.ToUniversalTime();
                    if (sUtc < dayEndUtc && eUtc > dayStartUtc)
                        occupiedIntervals.Add((sUtc, eUtc));
                }
            }

            // 7) build slots per schedule block
            var result = new List<TimeSlot>();

            foreach (var schedule in schedules)
            {
                // Monta o horário local do agendamento combinando DateOnly + TimeSpan
                var scheduleStartLocal = date.ToDateTime(TimeOnly.FromTimeSpan(schedule.Start));
                var scheduleEndLocal = date.ToDateTime(TimeOnly.FromTimeSpan(schedule.End));

                // apply override for the exact date, if present
                var ov = schedule.Overrides.FirstOrDefault(o => o.Date.Date == date.ToDateTime(TimeOnly.MinValue).Date);
                if (ov != null)
                {
                    if (ov.IsClosed)
                        continue; // entire schedule closed for the date

                    if (ov?.Start is TimeSpan oStart && ov?.End is TimeSpan oEnd)
                    {
                        // Ajusta a janela de trabalho pela override
                        scheduleStartLocal = date.ToDateTime(TimeOnly.FromTimeSpan(oStart));
                        scheduleEndLocal = date.ToDateTime(TimeOnly.FromTimeSpan(oEnd));
                    }

                }

                // convert schedule window to UTC for collision checks
                var scheduleStartUtc = TimeZoneInfo.ConvertTimeToUtc(scheduleStartLocal, tenantTz);
                var scheduleEndUtc = TimeZoneInfo.ConvertTimeToUtc(scheduleEndLocal, tenantTz);

                // iteration step uses schedule.IntervalMinutes
                var step = TimeSpan.FromMinutes(schedule.IntervalMinutes);
                for (var slotStartUtc = scheduleStartUtc; slotStartUtc + duration <= scheduleEndUtc; slotStartUtc = slotStartUtc.Add(step))
                {
                    var slotEndUtc = slotStartUtc + duration;

                    // slot should be within the day requested
                    if (slotStartUtc < dayStartUtc || slotEndUtc > dayEndUtc)
                        continue;

                    // if any occupied interval overlaps -> skip
                    var conflict = occupiedIntervals.Any(o => o.StartUtc < slotEndUtc && o.EndUtc > slotStartUtc);
                    if (conflict) continue;

                    // build local presentation values
                    var slotStartLocal = TimeZoneInfo.ConvertTimeFromUtc(slotStartUtc, tenantTz);
                    var slotEndLocal = TimeZoneInfo.ConvertTimeFromUtc(slotEndUtc, tenantTz);

                    result.Add(new TimeSlot(slotStartLocal, slotEndLocal));
                }
            }

            // sort ascending by Start
            result = result.OrderBy(x => x.Start).ToList();

            return result;
        }
    }
}