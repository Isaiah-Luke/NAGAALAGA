using NagaAlaga.Domain.Profiles;
using Supabase.Gotrue;

namespace NagaAlaga.Infrastructure.Auth;

public interface IAuthService
{
    bool IsAuthenticated { get; }
    User? CurrentUser { get; }
    Task<User?> SignUpAsync(string email, string password);
    Task<bool> SignInAsync(string email, string password);
    Task SignOutAsync();
    Task<ProfileDto?> GetCurrentProfileAsync();
}