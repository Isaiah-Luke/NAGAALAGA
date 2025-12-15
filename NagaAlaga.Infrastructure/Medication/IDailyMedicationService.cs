using NagaAlaga.Domain.Medication;

namespace NagaAlaga.Infrastructure.Medication
{
    public interface IDailyMedicationService
    {
        Task<IReadOnlyList<TodayMedicationDto>> GetTodayMedicationsAsync(Guid profileId, DateOnly? date = null);
    }
}
