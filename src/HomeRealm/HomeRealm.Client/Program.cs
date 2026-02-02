
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
// Removed Kiota client registrations (client projects were deleted)

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

// Note: external generated clients were removed. Add your API client registrations here if needed.


await builder.Build().RunAsync();
