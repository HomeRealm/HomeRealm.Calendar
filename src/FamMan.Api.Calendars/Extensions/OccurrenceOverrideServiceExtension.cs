using FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;
using FamMan.Api.Calendars.Services.OccurrenceOverrides;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class OccurrenceOverrideServiceExtension
{
  public static IServiceCollection AddOccurrenceOverrideServices(this IServiceCollection services)
  {
    services.TryAddTransient<IOccurrenceOverrideService, OccurrenceOverrideService>();
    services.TryAddScoped<IOccurrenceOverrideDataStore, OccurrenceOverrideDataStore>();
    return services;
  }
}
