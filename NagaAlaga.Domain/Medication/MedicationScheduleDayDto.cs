using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Medication
{
    public sealed class MedicationScheduleDayDto
    {
        public Guid Id { get; set; }

        public Guid ScheduleId { get; set; }

        /// <summary>
        /// 0 = Sunday, 6 = Saturday
        /// </summary>
        public int DayOfWeek { get; set; }
    }
}
