using HomeRealm.Api.Calendars.Dtos.RecurrenceRules;
using HomeRealm.Api.Calendars.Interfaces.RecurrenceRules;
using HomeRealm.Api.Calendars.Services.RecurrenceRules;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

public static class RecurrenceRuleServiceExtension
{
  public static IServiceCollection AddRecurrenceRuleServices(this IServiceCollection services)
  {
    services.TryAddTransient<IRecurrenceRuleService, RecurrenceRuleService>();
    services.TryAddScoped<IRecurrenceRuleDataStore, RecurrenceRuleDataStore>();
    services.TryAddTransient<IValidator<RecurrenceRuleDto>, RecurrenceRuleDtoValidator>();
    return services;
  }
}
