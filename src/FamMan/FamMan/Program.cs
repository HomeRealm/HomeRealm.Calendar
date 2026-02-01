using FamMan.Client.Pages;
using FamMan.Components;
using MudBlazor.Services;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using FamMan.Clients;

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
builder.Services.AddHttpClient("EventsApiClient", client =>
{
  client.BaseAddress = new Uri("https+http://famman-api-events");
}).AddServiceDiscovery();

builder.Services.AddHttpClient("ChoresApiClient", client =>
{
  client.BaseAddress = new Uri("https+http://famman-api-chores");
}).AddServiceDiscovery();


// Register API Clients
builder.Services.AddScoped(sp =>
{
  var authProvider = new AnonymousAuthenticationProvider();
  var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ChoresApiClient");
  var requestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
  return new EventsClient(requestAdapter);
});

builder.Services.AddScoped(sp =>
{
  var authProvider = new AnonymousAuthenticationProvider();
  var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ChoresApiClient");
  var requestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
  return new ChoresClient(requestAdapter);
});

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
