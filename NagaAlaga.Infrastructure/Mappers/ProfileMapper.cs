using NagaAlaga.Domain.Enums;
using NagaAlaga.Domain.Profiles;
using NagaAlaga.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagaAlaga.Infrastructure.Mappers
{
    internal static class ProfileMapper
    {
        public static ProfileDto ToDto(this SupabaseProfile model)
        => new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            Suffix = model.Suffix,
            Birthdate = model.Birthdate,
            Alive = model.Alive,
            Gender = Enum.Parse<Gender>(model.Gender, true),
            BloodType = model.BloodType != null
                ? Enum.Parse<BloodType>(model.BloodType, true)
                : null
        };

        public static SupabaseProfile ToModel(this ProfileDto dto)
        => new()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            Suffix = dto.Suffix,
            Birthdate = dto.Birthdate,
            Alive = dto.Alive,
            Gender = dto.Gender.ToString().ToLower(),
            BloodType = dto.BloodType?.ToString().ToLower()
        };
    }
}
