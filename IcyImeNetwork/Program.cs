using System.Globalization;
using BlazorAnimate;
using Blazored.LocalStorage;
using IcyImeNetwork;
using IcyImeNetwork.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Microsoft.Extensions.Localization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add localization and local storage services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.Configure<AnimateOptions>(options =>
{
    options.Animation = Animations.FadeDown;
    options.Duration = TimeSpan.FromSeconds(2);
});
builder.Services.AddSingleton<LanguageShuffle>();

var host = builder.Build();
await host.SetDefaultCulture(); // Retrieves local storage value and sets the thread's current culture.
await host.RunAsync();
