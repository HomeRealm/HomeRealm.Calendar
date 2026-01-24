using FamMan.Api.Calendars.Dtos.RecurrenceRules;
using FamMan.Api.Calendars.Interfaces.RecurrenceRules;
using FamMan.Api.Calendars.Services.RecurrenceRules;
using FamMan.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

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
