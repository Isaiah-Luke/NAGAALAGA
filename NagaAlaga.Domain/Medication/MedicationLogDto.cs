using NagaAlaga.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Medication
{
    public sealed class MedicationLogDto
    {
        public Guid Id { get; set; }

        public Guid MedicationId { get; set; }

        public DateOnly LogDate { get; set; }
        public TimeOnly TimeOfDay { get; set; }

        public MedicationLogStatus Status { get; set; }

        public DateTimeOffset LoggedAt { get; set; }

        public string? Notes { get; set; }
    }
}
