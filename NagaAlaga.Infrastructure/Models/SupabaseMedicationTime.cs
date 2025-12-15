using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Infrastructure.Models
{
    [Table("medication_times")]
    internal sealed class SupabaseMedicationTime : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("medication_id")]
        public Guid MedicationId { get; set; }

        [Column("time_of_day")]
        public TimeOnly TimeOfDay { get; set; }
    }
}
