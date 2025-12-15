using NagaAlaga.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Medication
{
    public sealed class TodayMedicationDto
    {
        public Guid MedicationId { get; set; }
        public string Name { get; set; } = default!;
        public string Dosage { get; set; } = default!;
        public string? Condition { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public List<TimeOnly> Times { get; set; } = new();
        public Dictionary<TimeOnly, MedicationLogStatus> TimeStatus { get; set; } = new();
    }
}
