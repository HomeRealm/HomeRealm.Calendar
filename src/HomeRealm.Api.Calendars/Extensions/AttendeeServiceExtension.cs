using HomeRealm.Api.Calendars.Dtos.Attendees;
using HomeRealm.Api.Calendars.Interfaces.Attendees;
using HomeRealm.Api.Calendars.Services.Attendees;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

public static class AttendeeServiceExtension
{
  public static IServiceCollection AddAttendeeServices(this IServiceCollection services)
  {
    services.TryAddTransient<IAttendeeService, AttendeeService>();
    services.TryAddScoped<IAttendeeDataStore, AttendeeDataStore>();
    services.TryAddTransient<IValidator<AttendeeDto>, AttendeeDtoValidator>();
    return services;
  }
}

