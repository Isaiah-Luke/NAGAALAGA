using NagaAlaga.Domain.Profiles;
using NagaAlaga.Infrastructure.Mappers;
using NagaAlaga.Infrastructure.Models;
using Supabase;

namespace NagaAlaga.Infrastructure.Profile
{
    public class ProfileService
    {
        private readonly Client _supabase;

        public ProfileService(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task UpdateProfileAsync(ProfileDto profile)
        {
            await _supabase
                .From<SupabaseProfile>()
                .Where(p => p.Id == profile.Id)
                .Update(profile.ToModel());
        }

        public async Task<ProfileDto?> GetProfileAsync(Guid id)
        {
            var profile = await _supabase
                .From<SupabaseProfile>()
                .Where(p => p.Id == id)
                .Single();

            return profile?.ToDto();
        }
    }

}
