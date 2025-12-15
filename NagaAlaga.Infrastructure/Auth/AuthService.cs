using NagaAlaga.Domain.Profiles;
using NagaAlaga.Infrastructure.Mappers;
using NagaAlaga.Infrastructure.Models;
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

    public User? CurrentUser => _supabase.Auth.CurrentUser;

    // -------------------------------
    // SIGN UP
    // -------------------------------
    /// <summary>
    /// Signs up the user and returns the user object if successful
    /// </summary>
    public async Task<User?> SignUpAsync(string email, string password)
    {
        try
        {
            var result = await _supabase.Auth.SignUp(email, password);

            if (result.User == null)
                return null;

            // Supabase trigger will create a placeholder profile automatically
            return result.User;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SignUp error: {ex.Message}");
            return null;
        }
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