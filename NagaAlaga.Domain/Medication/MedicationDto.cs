using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Medication
{
    public sealed class MedicationDto
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }

        public string Name { get; set; } = default!;
        public string Dosage { get; set; } = default!;

        public string? Condition { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string? Notes { get; set; }
    }
}
