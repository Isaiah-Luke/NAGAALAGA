using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NagaAlaga.Frontend;
using NagaAlaga.Infrastructure.Auth;
using NagaAlaga.Infrastructure.Services;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Supabase client
builder.Services.AddScoped(sp =>
{
    var url = builder.Configuration["Supabase:Url"]!;
    var key = builder.Configuration["Supabase:AnonKey"]!;

    var client = new Client(url, key);
    return client;
});

builder.Services.AddScoped<IAuthService, AuthService>();

// Infrastructure services
builder.Services.AddScoped<ISupabaseMedicationService, SupabaseMedicationService>(); // registers ISupabaseMedicationService
builder.Services.AddScoped<IDailyMedicationService, DailyMedicationService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
