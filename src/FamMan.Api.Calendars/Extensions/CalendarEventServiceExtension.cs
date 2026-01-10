using FamMan.Api.Calendars.Interfaces.CalendarEvents;
using FamMan.Api.Calendars.Services.CalendarEvents;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class CalendarEventServiceExtension
{
  public static IServiceCollection AddCalendarEventServices(this IServiceCollection services)
  {
    services.TryAddTransient<ICalendarEventService, CalendarEventService>();
    services.TryAddScoped<ICalendarEventDataStore, CalendarEventDataStore>();
    return services;
  }
}
