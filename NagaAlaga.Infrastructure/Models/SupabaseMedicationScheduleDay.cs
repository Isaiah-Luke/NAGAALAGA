using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace NagaAlaga.Infrastructure.Models
{
    [Table("medication_schedule_days")]
    internal sealed class SupabaseMedicationScheduleDay : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("schedule_id")]
        public Guid ScheduleId { get; set; }

        [Column("day_of_week")]
        public int DayOfWeek { get; set; }
    }
}
