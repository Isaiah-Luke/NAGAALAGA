using NagaAlaga.Domain.Profiles;
using Supabase.Gotrue;

namespace NagaAlaga.Infrastructure.Services;

public interface IAuthService
{
    Task<ProfileDto?> SignUpAsync(string email, string password, ProfileDto profile);
    Task<bool> SignInAsync(string email, string password);
    Task SignOutAsync();

    Task<ProfileDto?> GetCurrentProfileAsync();
    bool IsAuthenticated { get; }
}