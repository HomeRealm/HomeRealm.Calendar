using FamMan.Api.Calendars.Interfaces.Attendee;
using FamMan.Api.Calendars.Services.Attendee;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class AttendeeServiceExtension
{
  public static IServiceCollection AddAttendeeServices(this IServiceCollection services)
  {
    services.TryAddTransient<IAttendeeService, AttendeeService>();
    services.TryAddScoped<IAttendeeDataStore, AttendeeDataStore>();
    return services;
  }
}
