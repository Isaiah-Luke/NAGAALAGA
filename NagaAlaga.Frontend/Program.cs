using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NagaAlaga.Frontend;
using NagaAlaga.Infrastructure.Auth;
using NagaAlaga.Infrastructure.Medication;
using NagaAlaga.Infrastructure.Profile;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Supabase client
// TODO-NEWT: Move to secure storage
var url = "https://raewyanvspgofwkvmjic.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJhZXd5YW52c3Bnb2Z3a3ZtamljIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjU3MTE2NTYsImV4cCI6MjA4MTI4NzY1Nn0.qpi9pwbJegcnqHYpGi35WDPhFyg4PAkHCIvJaptbK_A";

// ✅ Create & initialize FIRST
var supabase = new Client(url, key);
await supabase.InitializeAsync();

// ✅ Then register
builder.Services.AddScoped<Client>(_ => supabase);
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAuthService, AuthService>();

// Infrastructure services
builder.Services.AddScoped<ISupabaseMedicationService, SupabaseMedicationService>(); // registers ISupabaseMedicationService
builder.Services.AddScoped<IDailyMedicationService, DailyMedicationService>();
builder.Services.AddScoped<ProfileService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
