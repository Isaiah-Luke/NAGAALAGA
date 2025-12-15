using NagaAlaga.Domain.Enums;
using NagaAlaga.Domain.Medication;

namespace NagaAlaga.Infrastructure.Medication
{
    public sealed class DailyMedicationService : IDailyMedicationService
    {
        private readonly ISupabaseMedicationService _medicationService;

        public DailyMedicationService(ISupabaseMedicationService medicationService)
        {
            _medicationService = medicationService;
        }
        public async Task<IReadOnlyList<TodayMedicationDto>> GetTodayMedicationsAsync(Guid profileId, DateOnly? date = null)
        {
            var today = date ?? DateOnly.FromDateTime(DateTime.UtcNow);

            // 1️ Load medications for profile
            var medications = await _medicationService.GetMedicationsAsync(profileId);

            var result = new List<TodayMedicationDto>();

            // 2️ Preload all schedules for all medications
            var allSchedules = new Dictionary<Guid, List<MedicationScheduleDto>>();
            var allScheduleDays = new Dictionary<Guid, List<MedicationScheduleDayDto>>();

            foreach (var med in medications)
            {
                var schedules = await _medicationService.GetSchedulesAsync(med.Id);
                allSchedules[med.Id] = schedules;

                // Preload schedule days for weekly schedules
                var weeklySchedules = schedules.Where(s => s.ScheduleType == ScheduleType.Weekly);
                foreach (var s in weeklySchedules)
                {
                    var days = await _medicationService.GetScheduleDaysAsync(s.Id);
                    allScheduleDays[s.Id] = days;
                }
            }

            // 3️ Iterate medications
            foreach (var med in medications)
            {
                // Skip medications not active today
                if (today < med.StartDate || (med.EndDate.HasValue && today > med.EndDate.Value))
                    continue;

                var schedules = allSchedules[med.Id];
                bool isScheduledToday = false;

                foreach (var s in schedules)
                {
                    if (await IsScheduledTodayAsync(s, today, allScheduleDays))
                    {
                        isScheduledToday = true;
                        break;
                    }
                }

                if (!isScheduledToday)
                    continue;

                // 4️⃣ Load times & logs
                var times = await _medicationService.GetTimesAsync(med.Id);
                var logs = await _medicationService.GetLogsForDateAsync(med.Id, today);

                var timeStatus = new Dictionary<TimeOnly, MedicationLogStatus>();
                foreach (var t in times)
                {
                    var log = logs.FirstOrDefault(l => l.TimeOfDay == t.TimeOfDay);
                    timeStatus[t.TimeOfDay] = log?.Status ?? MedicationLogStatus.Pending;
                }

                result.Add(new TodayMedicationDto
                {
                    MedicationId = med.Id,
                    Name = med.Name,
                    Dosage = med.Dosage,
                    Condition = med.Condition,
                    StartDate = med.StartDate,
                    EndDate = med.EndDate,
                    Times = times.Select(t => t.TimeOfDay).ToList(),
                    TimeStatus = timeStatus
                });
            }

            return result;
        }

        private static Task<bool> IsScheduledTodayAsync(
            MedicationScheduleDto schedule,
            DateOnly today,
            Dictionary<Guid, List<MedicationScheduleDayDto>> allScheduleDays)
        {
            switch (schedule.ScheduleType)
            {
                case ScheduleType.Daily:
                    return Task.FromResult(true);

                case ScheduleType.Interval:
                    if (!schedule.IntervalDays.HasValue) return Task.FromResult(true);
                    var daysSinceStart = (today.ToDateTime(TimeOnly.MinValue) - schedule.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
                    return Task.FromResult(daysSinceStart % schedule.IntervalDays.Value == 0);

                case ScheduleType.Weekly:
                    if (!allScheduleDays.TryGetValue(schedule.Id, out var days)) return Task.FromResult(false);
                    int todayDayOfWeek = (int)today.DayOfWeek; // 0 = Sunday, 6 = Saturday
                    return Task.FromResult(days.Any(d => d.DayOfWeek == todayDayOfWeek));

                default:
                    return Task.FromResult(false);
            }
        }
    }
}
