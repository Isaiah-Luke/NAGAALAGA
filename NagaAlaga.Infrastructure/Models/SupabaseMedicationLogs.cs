using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Infrastructure.Models
{
    [Table("medication_logs")]
    internal sealed class SupabaseMedicationLog : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("medication_id")]
        public Guid MedicationId { get; set; }

        [Column("log_date")]
        public DateOnly LogDate { get; set; }

        [Column("time_of_day")]
        public TimeOnly TimeOfDay { get; set; }

        [Column("status")]
        public string Status { get; set; } = default!;

        [Column("logged_at")]
        public DateTimeOffset LoggedAt { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }
    }
}
