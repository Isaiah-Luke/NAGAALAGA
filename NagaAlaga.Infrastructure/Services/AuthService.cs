using NagaAlaga.Domain.Profiles;
using NagaAlaga.Infrastructure.Mappers;
using NagaAlaga.Infrastructure.Models;
using NagaAlaga.Infrastructure.Services;
using Supabase;
using Supabase.Gotrue;

namespace NagaAlaga.Infrastructure.Auth;

public sealed class AuthService : IAuthService
{
    private readonly Supabase.Client _supabase;

    public AuthService(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public bool IsAuthenticated =>
        _supabase.Auth.CurrentUser != null;

    // -------------------------------
    // SIGN UP
    // -------------------------------
    public async Task<ProfileDto?> SignUpAsync(
        string email,
        string password,
        ProfileDto profile)
    {
        var result = await _supabase.Auth.SignUp(email, password);

        if (result.User == null)
            return null;

        profile.Id = Guid.Parse(result.User.Id);

        await _supabase
            .From<SupabaseProfile>()
            .Insert(profile.ToModel());

        return profile;
    }

    // -------------------------------
    // SIGN IN
    // -------------------------------
    public async Task<bool> SignInAsync(string email, string password)
    {
        await _supabase.Auth.SignIn(email, password);

        // Session is stored internally by Supabase
        return _supabase.Auth.CurrentUser != null;
    }

    // -------------------------------
    // SIGN OUT
    // -------------------------------
    public async Task SignOutAsync()
    {
        await _supabase.Auth.SignOut();
    }

    // -------------------------------
    // GET CURRENT PROFILE
    // -------------------------------
    public async Task<ProfileDto?> GetCurrentProfileAsync()
    {
        var user = _supabase.Auth.CurrentUser;
        if (user == null)
            return null;

        var id = Guid.Parse(user.Id);

        var profile = await _supabase
            .From<SupabaseProfile>()
            .Where(p => p.Id == id)
            .Single();

        return profile?.ToDto();
    }
}