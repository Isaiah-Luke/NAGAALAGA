using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Domain.Profiles
{
    public sealed class ProfileRelationshipDto
    {
        public Guid Id { get; set; }

        public Guid OwnerProfileId { get; set; }
        public Guid RelatedProfileId { get; set; }

        public string RelationshipType { get; set; } = default!;

        public bool CanViewMedications { get; set; }
        public bool CanManageMedications { get; set; }
    }
}
