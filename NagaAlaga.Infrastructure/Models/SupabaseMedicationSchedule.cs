using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace NagaAlaga.Infrastructure.Models
{
    [Table("medication_schedules")]
    internal sealed class SupabaseMedicationSchedule : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("medication_id")]
        public Guid MedicationId { get; set; }

        [Column("schedule_type")]
        public string ScheduleType { get; set; } = default!;

        [Column("interval_days")]
        public int? IntervalDays { get; set; }

        [Column("start_date")]
        public DateOnly StartDate { get; set; }

        [Column("end_date")]
        public DateOnly? EndDate { get; set; }
    }
}
