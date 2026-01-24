using FamMan.Api.Calendars.Dtos.CalendarEvents;
using FamMan.Api.Calendars.Interfaces.CalendarEvents;
using FamMan.Api.Calendars.Services.CalendarEvents;
using FamMan.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

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
