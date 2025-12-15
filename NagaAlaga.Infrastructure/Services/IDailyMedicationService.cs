using NagaAlaga.Domain.Medication;

namespace NagaAlaga.Infrastructure.Services
{
    public interface IDailyMedicationService
    {
        Task<IReadOnlyList<TodayMedicationDto>> GetTodayMedicationsAsync(Guid profileId, DateOnly? date = null);
    }
}
