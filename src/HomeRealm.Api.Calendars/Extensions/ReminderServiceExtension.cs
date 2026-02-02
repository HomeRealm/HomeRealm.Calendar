using HomeRealm.Api.Calendars.Dtos.Reminders;
using HomeRealm.Api.Calendars.Interfaces.Reminders;
using HomeRealm.Api.Calendars.Services.Reminders;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

public static class ReminderServiceExtension
{
  public static IServiceCollection AddReminderServices(this IServiceCollection services)
  {
    services.TryAddTransient<IReminderService, ReminderService>();
    services.TryAddScoped<IReminderDataStore, ReminderDataStore>();
    services.TryAddTransient<IValidator<ReminderDto>, ReminderDtoValidator>();
    return services;
  }
}

