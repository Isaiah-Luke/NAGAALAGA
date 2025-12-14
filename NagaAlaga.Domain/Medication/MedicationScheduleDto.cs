using NagaAlaga.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Medication
{
    public sealed class MedicationScheduleDto
    {
        public Guid Id { get; set; }

        public Guid MedicationId { get; set; }

        public ScheduleType ScheduleType { get; set; }

        public int? IntervalDays { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
