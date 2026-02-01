using FamMan.Client.Pages;
using FamMan.Components;
using MudBlazor.Services;
// Kiota-generated clients removed; remove related usings and registrations

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("config/proxy.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables("FAMMAN_")
    .AddCommandLine(args);
builder.AddServiceDefaults();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();
// Add MudBlazor services
builder.Services.AddMudServices();

// Configure HttpClients with Aspire service discovery for API clients
// Note: Events and Chores API clients were removed. If you need API clients,
// re-add them or replace these with alternative registrations.

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

app.MapDefaultEndpoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(FamMan.Client._Imports).Assembly);
app.MapReverseProxy();

app.Run();
