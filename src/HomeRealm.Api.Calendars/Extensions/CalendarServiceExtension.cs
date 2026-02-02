using HomeRealm.Api.Calendars.Dtos.Calendars;
using HomeRealm.Api.Calendars.Interfaces.Calendars;
using HomeRealm.Api.Calendars.Services.Calendars;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

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

