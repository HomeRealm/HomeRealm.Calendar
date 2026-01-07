using FamMan.Api.Calendars.Interfaces.RecurrenceRule;
using FamMan.Api.Calendars.Services.RecurrenceRule;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class RecurrenceRuleServiceExtension
{
  public static IServiceCollection AddRecurrenceRuleServices(this IServiceCollection services)
  {
    services.TryAddTransient<IRecurrenceRuleService, RecurrenceRuleService>();
    services.TryAddScoped<IRecurrenceRuleDataStore, RecurrenceRuleDataStore>();
    return services;
  }
}
