using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace NagaAlaga.Infrastructure.Models
{
    [Table("medications")]
    internal sealed class SupabaseMedication : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("profile_id")]
        public Guid ProfileId { get; set; }

        [Column("name")]
        public string Name { get; set; } = default!;

        [Column("dosage")]
        public string Dosage { get; set; } = default!;

        [Column("condition")]
        public string? Condition { get; set; }

        [Column("start_date")]
        public DateOnly StartDate { get; set; }

        [Column("end_date")]
        public DateOnly? EndDate { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }
    }
}
