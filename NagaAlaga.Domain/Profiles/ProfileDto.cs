using NagaAlaga.Domain.Enums;

namespace NagaAlaga.Domain.Profiles
{
    public sealed class ProfileDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = default!;
        public string? Suffix { get; set; }

        public DateOnly Birthdate { get; set; }
        public bool Alive { get; set; }

        public Gender Gender { get; set; }
        public BloodType? BloodType { get; set; }
    }
}
