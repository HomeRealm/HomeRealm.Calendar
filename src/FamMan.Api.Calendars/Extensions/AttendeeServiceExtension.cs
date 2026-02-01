using FamMan.Api.Calendars.Dtos.Attendees;
using FamMan.Api.Calendars.Interfaces.Attendees;
using FamMan.Api.Calendars.Services.Attendees;
using FamMan.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

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
