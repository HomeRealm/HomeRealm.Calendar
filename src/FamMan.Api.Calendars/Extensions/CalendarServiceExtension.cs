using FamMan.Api.Calendars.Dtos.Calendars;
using FamMan.Api.Calendars.Interfaces.Calendars;
using FamMan.Api.Calendars.Services.Calendars;
using FamMan.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class CalendarServiceExtension
{
  public static IServiceCollection AddCalendarServices(this IServiceCollection services)
  {
    services.TryAddTransient<ICalendarService, CalendarService>();
    services.TryAddScoped<ICalendarDataStore, CalendarDataStore>();
    services.TryAddTransient<IValidator<CalendarDto>, CalendarDtoValidator>();
    return services;
  }
}
