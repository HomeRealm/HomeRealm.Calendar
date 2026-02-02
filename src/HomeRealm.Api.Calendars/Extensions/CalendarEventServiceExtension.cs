using HomeRealm.Api.Calendars.Dtos.CalendarEvents;
using HomeRealm.Api.Calendars.Interfaces.CalendarEvents;
using HomeRealm.Api.Calendars.Services.CalendarEvents;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

public static class CalendarEventServiceExtension
{
  public static IServiceCollection AddCalendarEventServices(this IServiceCollection services)
  {
    services.TryAddTransient<ICalendarEventService, CalendarEventService>();
    services.TryAddScoped<ICalendarEventDataStore, CalendarEventDataStore>();
    services.TryAddTransient<IValidator<CalendarEventDto>, CalendarEventDtoValidator>();
    return services;
  }
}
