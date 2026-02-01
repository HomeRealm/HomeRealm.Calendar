using FamMan.Api.Calendars.Dtos.OccurrenceOverrides;
using FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;
using FamMan.Api.Calendars.Services.OccurrenceOverrides;
using FamMan.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class OccurrenceOverrideServiceExtension
{
  public static IServiceCollection AddOccurrenceOverrideServices(this IServiceCollection services)
  {
    services.TryAddTransient<IOccurrenceOverrideService, OccurrenceOverrideService>();
    services.TryAddScoped<IOccurrenceOverrideDataStore, OccurrenceOverrideDataStore>();
    services.TryAddTransient<IValidator<OccurrenceOverrideDto>, OccurrenceOverrideDtoValidator>();
    return services;
  }
}
