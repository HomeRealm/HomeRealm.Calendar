using FamMan.Api.Calendars.Interfaces.Reminders;
using FamMan.Api.Calendars.Services.Reminders;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class ReminderServiceExtension
{
  public static IServiceCollection AddReminderServices(this IServiceCollection services)
  {
    services.TryAddTransient<IReminderService, ReminderService>();
    services.TryAddScoped<IReminderDataStore, ReminderDataStore>();
    return services;
  }
}
