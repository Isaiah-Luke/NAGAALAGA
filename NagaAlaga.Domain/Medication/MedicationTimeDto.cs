using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Medication
{
    public sealed class MedicationTimeDto
    {
        public Guid Id { get; set; }

        public Guid MedicationId { get; set; }

        public TimeOnly TimeOfDay { get; set; }
    }
}
