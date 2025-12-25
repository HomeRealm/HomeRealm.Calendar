
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using FamMan.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

// Configure EventsClient to use YARP proxy path
var eventsApiPath = builder.Configuration["EventsApi:BaseUrl"] ?? "/api/events";

// Register IRequestAdapter using HttpClientRequestAdapter
builder.Services.AddScoped<IRequestAdapter>(sp =>
{
  var authProvider = new AnonymousAuthenticationProvider();
  var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
  return new HttpClientRequestAdapter(authProvider, httpClient: httpClient)
  {
    BaseUrl = eventsApiPath
  };
});

// Register Clients
builder.Services.AddScoped<EventsClient>();
builder.Services.AddScoped<ChoresClient>();


await builder.Build().RunAsync();
