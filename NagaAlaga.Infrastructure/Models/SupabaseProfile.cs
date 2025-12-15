using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace NagaAlaga.Infrastructure.Models
{
    [Table("profiles")]
    internal sealed class SupabaseProfile : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = default!;

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; } = default!;

        [Column("suffix")]
        public string? Suffix { get; set; }

        [Column("birthdate")]
        public DateOnly Birthdate { get; set; }

        [Column("alive")]
        public bool Alive { get; set; }

        [Column("gender")]
        public string Gender { get; set; } = default!;

        [Column("blood_type")]
        public string? BloodType { get; set; }
    }
}
