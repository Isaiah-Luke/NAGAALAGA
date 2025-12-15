using NagaAlaga.Domain.Medication;

namespace NagaAlaga.Infrastructure.Medication
{
    public interface ISupabaseMedicationService
    {
        Task<List<MedicationDto>> GetMedicationsAsync(Guid profileId);

        Task<List<MedicationScheduleDto>> GetSchedulesAsync(Guid medicationId);
        Task<List<MedicationScheduleDayDto>> GetScheduleDaysAsync(Guid scheduleId);

        Task<List<MedicationTimeDto>> GetTimesAsync(Guid medicationId);

        Task<List<MedicationLogDto>> GetLogsForDateAsync(
            Guid medicationId,
            DateOnly date
        );

        Task UpsertLogAsync(MedicationLogDto log);
    }
}
