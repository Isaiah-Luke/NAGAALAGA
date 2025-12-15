using NagaAlaga.Domain.Enums;
using NagaAlaga.Domain.Medication;
using NagaAlaga.Infrastructure.Models;
using Supabase;

namespace NagaAlaga.Infrastructure.Services
{
    public sealed class SupabaseMedicationService : ISupabaseMedicationService
    {
        private readonly Client _client;

        public SupabaseMedicationService(Client client)
        {
            _client = client;
        }

        public async Task<IReadOnlyList<MedicationDto>> GetMedicationsAsync(Guid profileId)
        {
            var result = await _client
                .From<SupabaseMedication>()
                .Where(m => m.ProfileId == profileId)
                .Get();

            return result.Models.Select(m => new MedicationDto
            {
                Id = m.Id,
                ProfileId = m.ProfileId,
                Name = m.Name,
                Dosage = m.Dosage,
                Condition = m.Condition,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Notes = m.Notes
            }).ToList();
        }

        public async Task<IReadOnlyList<MedicationScheduleDto>> GetSchedulesAsync(Guid medicationId)
        {
            var result = await _client
                .From<SupabaseMedicationSchedule>()
                .Where(s => s.MedicationId == medicationId)
                .Get();

            return result.Models.Select(s => new MedicationScheduleDto
            {
                Id = s.Id,
                MedicationId = s.MedicationId,
                ScheduleType = Enum.Parse<ScheduleType>(s.ScheduleType, true),
                IntervalDays = s.IntervalDays,
                StartDate = s.StartDate,
                EndDate = s.EndDate
            }).ToList();
        }

        public async Task<IReadOnlyList<MedicationScheduleDayDto>> GetScheduleDaysAsync(Guid scheduleId)
        {
            var result = await _client
                .From<SupabaseMedicationScheduleDay>()
                .Where(d => d.ScheduleId == scheduleId)
                .Get();

            return result.Models.Select(d => new MedicationScheduleDayDto
            {
                Id = d.Id,
                ScheduleId = d.ScheduleId,
                DayOfWeek = d.DayOfWeek
            }).ToList();
        }

        public async Task<IReadOnlyList<MedicationTimeDto>> GetTimesAsync(Guid medicationId)
        {
            var result = await _client
                .From<SupabaseMedicationTime>()
                .Where(t => t.MedicationId == medicationId)
                .Get();

            return result.Models.Select(t => new MedicationTimeDto
            {
                Id = t.Id,
                MedicationId = t.MedicationId,
                TimeOfDay = t.TimeOfDay
            }).ToList();
        }

        public async Task<IReadOnlyList<MedicationLogDto>> GetLogsForDateAsync(Guid medicationId, DateOnly date)
        {
            var result = await _client
                .From<SupabaseMedicationLog>()
                .Where(l => l.MedicationId == medicationId && l.LogDate == date)
                .Get();

            return result.Models.Select(l => new MedicationLogDto
            {
                Id = l.Id,
                MedicationId = l.MedicationId,
                LogDate = l.LogDate,
                TimeOfDay = l.TimeOfDay,
                Status = Enum.Parse<MedicationLogStatus>(l.Status, true),
                LoggedAt = l.LoggedAt,
                Notes = l.Notes
            }).ToList();
        }

        public async Task UpsertLogAsync(MedicationLogDto log)
        {
            var model = new SupabaseMedicationLog
            {
                Id = log.Id == Guid.Empty ? Guid.NewGuid() : log.Id,
                MedicationId = log.MedicationId,
                LogDate = log.LogDate,
                TimeOfDay = log.TimeOfDay,
                Status = log.Status.ToString().ToLower(),
                LoggedAt = DateTimeOffset.UtcNow,
                Notes = log.Notes
            };

            await _client
                .From<SupabaseMedicationLog>()
                .Upsert(model);
        }
    }
}